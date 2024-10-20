using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepository.Models
{
    public class FileValidationAttribute : ValidationAttribute
    {
        private readonly string[] _SupportingfileExtension;
        private readonly long _fileSize;

        public FileValidationAttribute(string[] fileExtension, long fileSize)
        {
            _SupportingfileExtension = fileExtension;
            _fileSize = fileSize;
        }
        protected override ValidationResult IsValid(object? value , ValidationContext context)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                return new ValidationResult("Please select a file, we can't proceed without a upload file!!");
            }

            if (file.Length > _fileSize)
            {
                return new ValidationResult($"File size exceeds the {_fileSize /(1024*1024)} MB Limit");
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (string.IsNullOrEmpty(fileExtension) || !_SupportingfileExtension.Contains(fileExtension))
            {
                return new ValidationResult("Invalid file type. Only Excel and PDF files are allowed");
            }


            //FileInfo fileInfo = new FileInfo();


            //if (fileInfo.Length == 0)
            //{
            //    Console.WriteLine("The file is empty.");
            //}

            
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                byte[] fileBytes = stream.ToArray();
                string content = System.Text.Encoding.UTF8.GetString(fileBytes);
                if (content.Length == 0)
                {
                    return new ValidationResult("This file is empty, Kindly try with different file");
                }
            }
            return ValidationResult.Success;
        }
    }
}
