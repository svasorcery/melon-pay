namespace MelonPay.Shared.Kernel.Types
{
    public abstract record TypeId
    {
        public Guid Value { get; }

        protected TypeId(Guid value) => Value = value;

        public bool IsEmpty() => Value == Guid.Empty;
        
        public static implicit operator Guid(TypeId typeId) => typeId.Value;

        public override string ToString() => Value.ToString();
    }
}
