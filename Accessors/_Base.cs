using ANIMALITOS_PHARMA_API.Models;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public class _BaseAccessor
    {

        internal AnimalitosPharmaContext _EntityContext = null;
        internal bool _loggedIn = false;


        public _BaseAccessor()
        {
            _loggedIn = false;
            _EntityContext = null;
        }

        internal void Connect(bool useRetryLogic)
        {
            // Connect to the database.
            _EntityContext = new AnimalitosPharmaContext(useRetryLogic);
            _loggedIn = true;
        }

        protected void SaveEntities()
        {
            _EntityContext.SaveChanges();

            if (_EntityContext != null)
                _EntityContext.SaveChanges();
        }
    }
}
