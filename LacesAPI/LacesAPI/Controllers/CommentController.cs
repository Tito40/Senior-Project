﻿using LacesAPI.Helpers;
using LacesAPI.Models.Request;
using LacesAPI.Models.Response;
using LacesDataModel.Image;
using LacesDataModel.Product;
using LacesDataModel.User;
using System;
using System.Configuration;
using System.IO;
using System.Web.Http;

namespace LacesAPI.Controllers
{
    [Authorize]
    public class CommentController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public LacesResponse AddComment(AddCommentRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Comment comment = new Comment();

                    // Confirm user and product exist
                    LacesDataModel.User.User user = new LacesDataModel.User.User(request.UserId);
                    LacesDataModel.Product.Product product = new LacesDataModel.Product.Product(request.ProductId);

                    comment.CreatedDate = DateTime.Now;
                    comment.ProductId = product.ProductId;
                    comment.Text = request.Text;
                    comment.UpdatedDate = DateTime.Now;
                    comment.UserId = user.UserId;

                    if (comment.Add())
                    {
                        response.Success = true;
                        response.Message = "Comment added succesfully.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "An error occurred when communicating with the database.";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch
            {
                response = new LacesResponse();
                response.Success = false;
                response.Message = "An unexpected error has occurred; please verify the format of your request.";
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        public LacesResponse UpdateComment(UpdateCommentRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Comment comment = new Comment(request.CommentId);

                    comment.Text = request.Text;
                    comment.UpdatedDate = DateTime.Now;

                    if (comment.Update())
                    {
                        response.Success = true;
                        response.Message = "Comment updated succesfully.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "An error occurred when communicating with the database.";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch (Exception ex)
            {
                response = new LacesResponse();
                response.Success = false;

                if (ex.Message.Contains("find comment"))
                {
                    response.Message = ex.Message;
                }
                else
                {
                    response.Message = "An unexpected error has occurred; please verify the format of your request.";
                }
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        public GetCommentResponse GetComment(CommentRequest request)
        {
            GetCommentResponse response = new GetCommentResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Comment comment = new Comment(request.CommentId);
                    LacesDataModel.User.User user = new LacesDataModel.User.User(comment.UserId);
                    Image userImage = new Image();

                    userImage.LoadAvatarByUserId(comment.UserId);

                    response.CreatedDate = comment.CreatedDate;
                    response.Text = comment.Text;
                    response.UpdatedDate = comment.UpdatedDate;

                    response.UserImage = new LacesAPI.Models.Response.ImageInfo();
                    response.UserImage.DateLastChanged = userImage.UpdatedDate;
                    response.UserImage.FileData = File.ReadAllBytes(userImage.FilePath);
                    response.UserImage.FileFormat = userImage.FileFormat;
                    response.UserImage.FileName = userImage.FileName;

                    response.UserName = user.UserName;

                    response.Success = true;
                    response.Message = "Comment details retrieved succesfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch (Exception ex)
            {
                response = new GetCommentResponse();
                response.Success = false;

                if (ex.Message.Contains("find comment") || ex.Message.Contains("find user"))
                {
                    response.Message = ex.Message;
                }
                else
                {
                    response.Message = "An unexpected error has occurred; please verify the format of your request.";
                }
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        public LacesResponse RemoveComment(CommentRequest request)
        {
            LacesResponse response = new LacesResponse();

            try
            {
                if (request.SecurityString == ConfigurationManager.AppSettings[Constants.APP_SETTING_SECURITY_TOKEN])
                {
                    Comment comment = new Comment(request.CommentId);

                    if (comment.Delete())
                    {
                        response.Success = true;
                        response.Message = "Comment removed succesfully.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "An error occurred when communicating with the database.";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid security token.";
                }
            }
            catch (Exception ex)
            {
                response = new LacesResponse();
                response.Success = false;

                if (ex.Message.Contains("find comment"))
                {
                    response.Message = ex.Message;
                }
                else
                {
                    response.Message = "An unexpected error has occurred; please verify the format of your request.";
                }
            }

            return response;
        }
    }
}