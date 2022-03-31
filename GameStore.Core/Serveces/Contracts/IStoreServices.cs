﻿using GameStore.Core.Models;

namespace GameStore.Core.Serveces.Contracts
{
    public interface IStoreServices
    {
        IEnumerable<GenreSortingModel> GetGenres();

        bool NameExists(string name);

        void Create(string name, 
            string developer, 
            string publisher, 
            string description, 
            decimal price, string imageUrl, 
            DateTime releasedate, 
            List<int> genreIds);
    }
}
