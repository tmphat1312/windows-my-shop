using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models;
public class User
{
    public int ID
    {
        get; set;
    }

    public string Name
    {
        get; set; 
    }

    public string Email
    {
        get; set; 
    }

    public string Password
    {
        get; set;

    }

    public string Role
    {

        get; set;
    }

    public string Image
    {
        get;set;
    }

    public DateTime CreateAt
    {
        get; set;
    }

    public DateTime PasswordUpdateAt
    {
        get; set;
    }

}
