using System;

namespace SMT.Models
{
    public abstract class SMTModel<T> : IComparable<SMTModel<T>> where T : SMTModel<T>, new()
    {
        /// <summary>
        /// Model's id.
        /// </summary>
        public virtual int ID { get; protected set; }

        protected SMTModel(int id) { ID = id; Init(); }

        protected SMTModel() : this(Math.Abs(Guid.NewGuid().ToString().GetHashCode())) { }

        /// <summary>
        /// Initalizes all model's properties with default values.
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// Normalizes all model's properties, e.g. removes any typos and redundant symbols.
        /// </summary>
        public abstract void Normalize();

        public override bool Equals(object other)
        {
            if (other == null || !GetType().Equals(other.GetType())) return false;
            return ((other as SMTModel<T>).ID == this.ID);
        }

        public override int GetHashCode() { return ID; }

        public T Clone()
        {
            T model = new T();
            CopyTo(model);
            return model;
        }

        public abstract void CopyTo(T model);

        public int CompareTo(SMTModel<T> other)
        {
            if (other == null) return 1;
            return ID.CompareTo(other.ID);
        }
    }
 
}
