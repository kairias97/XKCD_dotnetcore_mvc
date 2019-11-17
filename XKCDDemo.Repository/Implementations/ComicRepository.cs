﻿using System;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Repository.Interfaces;

namespace XKCDDemo.Repository.Implementations
{
    public class ComicRepository : IComicRepository
    {
        private readonly IXKCDApi _api;

        public ComicRepository(IXKCDApi api)
        {
            _api = api;
        }

        public async Task<ComicDetailVM> GetComicById(int comicId)
        {
            return await _api.GetComicById(comicId);
        }

        public async Task<ComicDetailVM> GetComicOfTheDay()
        {
            return await _api.GetComicOfTheDay();
        }

        public async Task<int?> GetFirstComicId()
        {
            return await _api.GetFirstComicId();
        }

        public async Task<int?> GetLastComicId()
        {
            return (await _api.GetComicOfTheDay())?.Num;
        }

        public async Task<int?> GetNextComicId(int comicId)
        {
            int? lastComicId = await GetLastComicId();
            int? firstComicId = await GetFirstComicId();
            if (lastComicId == null || firstComicId == null) return null;
            int? nextComicId = null;

            //It will fetch and get the reference id of the next available comic dynamically
            for (int i = comicId + 1; i <= lastComicId && i >= firstComicId; i++)
            {
                var targetComic = await _api.GetComicById(i);
                if (targetComic != null)
                {
                    nextComicId = targetComic.Num;
                    break;
                }
            }
            return nextComicId;
        }

        public async Task<int?> GetPreviousComicId(int comicId)
        {
            int? firstComicId = await GetFirstComicId();
            if (firstComicId == null) return null;
            //The previous comic id will be one id lower than the current comic id;
            return (comicId - 1 < firstComicId) ? (int?) null : comicId - 1; 
        }
    }
}
