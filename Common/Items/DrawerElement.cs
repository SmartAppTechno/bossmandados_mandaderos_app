using System;
namespace Common.Items
{
    public class DrawerElement
    {
        
        public string Name { get; set; }
        public DrawerPosition DrawerPosition { get; set; }
        public DrawerElement(string Name, DrawerPosition DrawerPosition)
        {
            this.Name = Name;
            this.DrawerPosition = DrawerPosition;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum DrawerPosition
    {
        PendingOrders, Perfil, ActiveOrder
    }
}
