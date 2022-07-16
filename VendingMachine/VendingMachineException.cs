namespace VendingMachine
{
    internal class VendingMachineException : Exception
    {
        public VendingMachineException()
        {

        }

        public VendingMachineException(string message) : base(message)
        {

        }

        public VendingMachineException(string message, Exception inner) : base(message, inner)
        {

        }
    }

    internal class VendingMachineControllerException : Exception
    {
        public VendingMachineControllerException()
        {

        }

        public VendingMachineControllerException(string message) : base(message)
        {

        }

        public VendingMachineControllerException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
