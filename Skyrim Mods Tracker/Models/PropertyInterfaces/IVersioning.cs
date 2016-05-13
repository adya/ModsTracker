
namespace SMT.Models.PropertyInterfaces
{
    /// <summary>
    /// Entity with version.
    /// </summary>
    interface IVersioning
    {
        Version Version { get; set; }
        bool HasValidVersion { get; }
    }
}
