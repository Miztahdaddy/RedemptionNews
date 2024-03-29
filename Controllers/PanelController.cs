﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedemptionNews.Data;
using RedemptionNews.Data.FileManager;
using RedemptionNews.Models;
using RedemptionNews.Repository;
using RedemptionNews.ViewModels;

namespace RedemptionNews.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public PanelController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _repo.GetPost((int)id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title =post.Title,
                    SubTitle = post.SubTitle,
                    Body = post.Body,
                    Author = post.Author,
                    CurrentImage = post.Image,
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                SubTitle = vm.SubTitle,
                Body = vm.Body,
                Author = vm.Author,
                Image = await _fileManager.SaveImage(vm.Image)
            };

            if(vm.Image == null)
            {
                post.Image = vm.CurrentImage;
            }
            else
            {
                post.Image = await _fileManager.SaveImage(vm.Image);
            }
            if (post.Id > 0)
            {
                _repo.UpdatePost(post);
            }
            else
            {
                _repo.AddPost(post);
            }
            
            if (await _repo.SaveChangesAsync())
            {
                    return RedirectToAction("Index");
            }

            else
            {
                return View(post);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
