using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using AspNetCoreGeneratedDocument;
using FitVerse.Web.Models;
using FitVerse.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace FitVerse.Web.Repositories.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
    }
}