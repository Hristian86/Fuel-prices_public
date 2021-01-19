namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Models.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<IpAddress> ipAddressRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<IpAddress> ipAddressRepository)
        {
            this.userRepository = userRepository;
            this.ipAddressRepository = ipAddressRepository;
        }

        public async Task<bool> ChangeUserName(string email, string userName)
        {
            var user = this.userRepository.All()
                .FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new ArgumentNullException("User does not exists");
            }

            user.UserName = userName;
            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveIpAddress(string ip, string info)
        {
            var containedIp = this.ipAddressRepository
                .All()
                .Where(i => i.Ip == ip)
                .FirstOrDefault();

            if (containedIp != null)
            {
                var visits = containedIp.Visits;
                containedIp.Visits = visits + 1;
                containedIp.ModifiedOn = DateTime.Now;
                if (containedIp.Info != info)
                {
                    containedIp.Info = containedIp.Info + info;
                }
                else
                {
                    containedIp.Info = info;
                }
            }
            else
            {
                var address = new IpAddress()
                {
                    Ip = ip,
                    Visits = 1,
                    Info = info,
                };

                await this.ipAddressRepository.AddAsync(address);
            }

            await this.ipAddressRepository.SaveChangesAsync();

            return false;
        }

        public async Task<bool> IpAddress(string ip, string email)
        {
            var user = this.userRepository.All()
                .Where(u => u.Email == email)
                .FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentNullException("User does not exists");
            }
            else
            {
                IpAddress existsIp = this.ipAddressRepository.All()
                .Where(x => x.Ip == ip)
                .FirstOrDefault();

                if (existsIp != null)
                {
                    existsIp.Email = user.Email;
                    existsIp.UserId = user.Id;
                    this.ipAddressRepository.Update(existsIp);

                    int currentCountsOfUserLogin = user.LoginsCount;
                    user.LoginsCount = currentCountsOfUserLogin += 1;
                    this.userRepository.Update(user);
                    await this.userRepository.SaveChangesAsync();
                    return true;
                }
                else
                {
                    // To Do: ask the user for the unknown address login
                    var address = new IpAddress()
                    {
                        Ip = ip,
                        UserId = user.Id,
                        User = user,
                        Email = email,
                    };

                    user.IpAddresses.Add(address);
                    int currentCountsOfUserLogin = user.LoginsCount;
                    user.LoginsCount = currentCountsOfUserLogin += 1;
                    this.userRepository.Update(user);
                    await this.userRepository.SaveChangesAsync();
                    return false;
                }
            }
        }
    }
}
