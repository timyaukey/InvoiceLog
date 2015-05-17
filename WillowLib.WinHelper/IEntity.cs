using System;

namespace WillowLib.WinHelper
{
    public interface IEntity
    {
        string Validate();
        bool HasData { get; }
    }
}
