using System.Threading;

namespace Midou.Utility.Tasks
{
    public class CancellationTokenMagazine
    {
        public CancellationToken Token => _cts.Token;
        public bool IsNull => _cts == null;
        public bool NotInUsed => IsNull || _cts.IsCancellationRequested;


        private CancellationTokenSource _cts;

        public CancellationTokenMagazine()
        {
        }
        public CancellationTokenMagazine(bool fire)
        {
            if (fire) { New(); }
        }

        public void New()
        {
            Dispose();
            _cts = new CancellationTokenSource();
        }

        public void New(CancellationToken linkedToken)
        {
            Dispose();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(linkedToken);
        }

        public void Cancel()
        {
            if (_cts is { IsCancellationRequested: false })
            {
                _cts.Cancel();
            }
        }

        public void End()
        {
            Cancel();
            Dispose();
        }

        void Dispose()
        {
            if (_cts != null)
            {
                _cts.Dispose();
                _cts = null;
            }
        }
    }
}
