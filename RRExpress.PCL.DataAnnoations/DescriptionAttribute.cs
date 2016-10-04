namespace System.ComponentModel {
    public class DescriptionAttribute : Attribute {

        public string Description { get; }

        public DescriptionAttribute(string description) {
            this.Description = description;
        }

    }
}
