namespace Compilator
{
    //The definitions for the states of codes
    public abstract class State
    {
        protected Error error { get; set; }
        public Error Err
        { 
            get { return error; }
        }

        public override string ToString()
        {
            return error == null ? "OK" : error.ToString();
        }
    }
    //there is not any error in the code
    public class CompilationStateOK:State
    {
        public CompilationStateOK() { error = null; }
    }
    //there is an error in the code
    public class CompilationStateERROR:State
    {
        public CompilationStateERROR(Error error) { this.error = error; }
    }
}