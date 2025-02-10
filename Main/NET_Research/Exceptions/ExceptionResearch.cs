namespace Main.NET_Research.Exceptions
{
    internal class ExceptionResearch
    {
        public void Run()
        {
            CheckFinallyWithDivisionByZeroException();
        }

        private void CheckFinallyWithDivisionByZeroException()
        {
            try
            {
                float a = 0;
                float b = 1 / a;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Finally");
            }
        }

        private void CheckFinallyWithStackOverflowException()
        {
            try
            {
                RecurciveFunc();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Finally");
            }
        }

        private void RecurciveFunc()
        {
            RecurciveFunc();
        }
    }
}
