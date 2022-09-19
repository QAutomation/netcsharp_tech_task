using System.Threading.Tasks;

namespace PBITracker.Clients
{
    public interface INotifier
    {
        Task Notify(string data);
    }
}