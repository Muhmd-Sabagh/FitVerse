using System.Reflection.Metadata.Ecma335;
using E_commerce_Khotwa.Models;
using E_commerce_Khotwa.Repository;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace E_commerce_Khotwa.UnitOfWorks
{
    public class UnitOfWork
    {
        //GenericRepository<Cart> CartRepo;
        CartRepository cartRepository;
        MarkITIContext db;
        public UnitOfWork(MarkITIContext  _db)
        {
            db = _db;
        }

        public CartRepository CartRepository { 
            get {
                if (cartRepository == null) 
                    cartRepository = new CartRepository(db);
                return cartRepository; 
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
