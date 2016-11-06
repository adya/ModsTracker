using SMT.Managers;
using SMT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.ViewModels
{
    static class Scheduler
    {
        public class UpdateProgress
        {
            public Mod Mod { get; set; }
            public Source Source { get; set; }
            public int TotalMods { get; set; }
            public int CurrentMod { get; set; }
            public int TotalSources { get; set; }
            public int CurrentSource { get; set; }
            public bool IsModCompleted { get; set; }
            public bool IsSourceCompleted { get; set; }
        }

        public static Task CheckModTask(IProgress<UpdateProgress> progress, params Mod[] mods)
        {
            var tasks = new List<Task>();
            foreach (var item in mods.Select((Mod, Index) => new { Mod, Index }))
            {
                var modProgress = new UpdateProgress()
                {
                    Mod = item.Mod,
                    CurrentMod = item.Index,
                    TotalMods = mods.Length
                };
                foreach (var src in item.Mod.Sources)
                {
                    tasks.Add(CheckSourceTask(progress, modProgress, src));
                }
            }
            return Task.WhenAll(tasks);
        }

        public static Task CheckSourceTask(IProgress<UpdateProgress> progress, Mod mod, params Source[] sources)
        {
            var modProgress = new UpdateProgress()
            {
                Mod = mod,
                CurrentMod = 0,
                TotalMods = 1,
            };
            return CheckSourceTask(progress, modProgress, sources);
        }

        private static Task CheckSourceTask(IProgress<UpdateProgress> progress, UpdateProgress modProgress, params Source[] sources)
        {
            var tasks = new List<Task>();
            foreach (var item in sources.Select((Source, Index) => new { Source, Index }))
            {
                var sourceProgress = new UpdateProgress()
                {
                    Source = item.Source,
                    CurrentSource = item.Index,
                    TotalSources = sources.Length,
                    Mod = modProgress.Mod,
                    CurrentMod = modProgress.CurrentMod,
                    TotalMods = modProgress.TotalMods
                };
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    if (progress != null) progress.Report(sourceProgress);
                    sourceProgress.Mod.State = ModState.Updating;
                    item.Source.CheckUpdate();
                    sourceProgress.IsSourceCompleted = true;
                    sourceProgress.Source.UpdateRelativeState(sourceProgress.Mod);
                    sourceProgress.IsModCompleted = (item.Index == sources.Length - 1);
                    if (sourceProgress.IsModCompleted)
                        sourceProgress.Mod.UpdateState(false);
                    if (progress != null) progress.Report(sourceProgress);
                }));
            }
            return Task.WhenAll(tasks);
        }

    }
}
