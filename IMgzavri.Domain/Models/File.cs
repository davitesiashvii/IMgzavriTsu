using IMgzavri.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMgzavri.Domain.Models
{
    public class File
    {
        public Guid Id { get; private set; }

        public Guid CorrelationId { get; private set; }

        public string? Name { get; private set; }

        public string? Extension { get; private set; }

        public string? ContentType { get; private set; }

        public long Size { get; private set; }

        public DateTimeOffset CreateDate { get; private set; }

        public Guid? CreateUserId { get; private set; }

        public DateTimeOffset? DeleteDate { get; private set; }

        public Guid? DeleteUserId { get; private set; }


        private File()
        {
        }

        private File(Guid id, string name, string extension, string contentType, long size, DateTimeOffset createDate, Guid createUserId, Guid correlationId)
        {
            Id = id;
            Name = name;
            Extension = extension;
            ContentType = contentType;
            Size = size;
            CreateDate = createDate;
            CreateUserId = createUserId;
            CorrelationId = correlationId;
        }

        public static File Create(string name, string extension, string contentType, long size, Guid createUserId, Guid correlationId)
        {
            var id = Guid.NewGuid();

            var newName = string.IsNullOrWhiteSpace(name) ? id.ToString() : $"{id}_{name}";

            var file = new File(id, newName, extension, contentType, size, DateTimeOffset.Now, createUserId,
                correlationId);

            Validate(file);

            return file;
        }

        public void Delete(Guid deleteUserId)
        {
            if (deleteUserId == default)
                throw new DomainException("Delete user id is required", ExceptionLevel.Fatal);

            if (DeleteDate.HasValue)
                throw new DomainException("File is already removed", ExceptionLevel.Fatal);

            DeleteDate = DateTimeOffset.Now;
            DeleteUserId = deleteUserId;
        }

        private static void Validate(File file)
        {
            if (string.IsNullOrWhiteSpace(file.Name))
                throw new DomainException("Name is required", ExceptionLevel.Fatal);

            if (file.Id == default)
                throw new DomainException("Id is required", ExceptionLevel.Fatal);

            if (file.CreateUserId == default)
                throw new DomainException("Creator id is required", ExceptionLevel.Fatal);

            if (file.CorrelationId == default)
                throw new DomainException("Correlation id is required", ExceptionLevel.Fatal);

            if (file.CreateDate == default)
                throw new DomainException("Create date is required", ExceptionLevel.Fatal);

            if (file.Size == default)
                throw new DomainException("Size is required", ExceptionLevel.Fatal);

            if (string.IsNullOrWhiteSpace(file.Extension))
                throw new DomainException("Extension is required", ExceptionLevel.Fatal);

            if (string.IsNullOrWhiteSpace(file.ContentType))
                throw new DomainException("Content type is required", ExceptionLevel.Fatal);
        }
    }
}
