using EstimasionSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstimasionSS.DataContext
{
    public class PlayerRepository
    {
        EstimasionSS.DataContext.Entities entitiesDb;

        public PlayerRepository()
        {
            entitiesDb = new Entities();
        }
        public Player Add(Models.PlayerModel player)
        {
            try
            {
                Player entity = AutoMapper.Mapper.Map<Player>(player);

                entitiesDb.Players.Add(entity);
                entitiesDb.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Player> GetByUserId(string userId)
        {
            var players = (from p in entitiesDb.Players
                           where p.UserId == userId
                           select p).ToList();
            return  players;
        }
    }
}