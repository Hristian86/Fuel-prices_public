namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserService
    {
        public Task<bool> IpAddress(string ip, string email);

        public Task<bool> ChangeUserName(string email, string userName);

        public Task<bool> SaveIpAddress(string ip, string info);
    }
}
