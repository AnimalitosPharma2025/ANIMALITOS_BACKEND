using ANIMALITOS_PHARMA_API.Contracts;
using Microsoft.EntityFrameworkCore;
using XAct;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        //public IEnumerable<Notification> GetListNotification(NotificationFilter filter)
        //{
        //    IQueryable<Models.Notification> query = from m in _EntityContext.Notifications select m;
        //    query = query.AsNoTracking();

        //    if (filter.Id > 0)
        //        query = query.Where(m => m.Id == filter.Id);
        //    if(!string.IsNullOrEmpty(filter.Title))
        //        query = query.Where(m => m.Title == filter.Title);
        //    if (filter.StatusId != 0)
        //        query = query.Where(m => m.StatusId == filter.StatusId);

        //    if (!string.IsNullOrWhiteSpace(filter.SortColumn))
        //        query = query.OrderBy(query => filter.SortColumn);

        //    if (filter.PagingBegin > -1)
        //        query = query.Skip(filter.PagingBegin);
        //    if (filter.PagingRange > -1)
        //        query = query.Take(filter.PagingRange);

        //    var tempStuff = query.ToList();
        //    List<AddressBook> context = new List<Contracts.AddressBook>();
        //    foreach (Models.AddressBook tempitem in tempStuff)
        //        context.Add(ConvertAddressBook_ToAccessorContract(tempitem));

        //    return context;
        //}
    }
}
