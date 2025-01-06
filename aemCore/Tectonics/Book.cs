using AEM;
using AEM.Tectonics;
using Interfaces;
using System;
using System.Collections.Generic;

namespace MbCore
{
    /// <summary>
    /// Represents a book entity in the application.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the unique database identifier for the book. Maintained by the entity framework
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// Gets or sets the reference ID for the book. Typically defined by the owning archive
        /// </summary>
        public string RefId { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of the first entry in the book.
        /// </summary>
        public DateOnly? StartDate { get; internal set; }
        
        /// <summary>
        /// Gets or sets the date of the last entry in the book.
        /// </summary>
        public DateOnly? EndDate { get; internal set; }

        /// <summary>
        /// Gets or sets the link to detailed book information, typically pointing to an external resource.
        /// </summary>
        public string BookInfoLink { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets or sets notes associated with this book.
        /// </summary>
        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the book, represented by the <see cref="BookType"/> enumeration.
        /// </summary>
        public BookType BookType { get; internal set; } = BookType.None;

        /// <summary>
        /// Gets or sets the prefix for page links associated with this book.
        /// This is used to construct complete links to individual pages of the book.
        /// </summary>
        public string ImageLinkPrefix { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets or sets the parish associated with the book. This is a required navigation property.
        /// </summary>
        virtual public Parish Parish { get; internal set; } = null!;

        /// <summary>
        /// Gets or sets the collection of pages belonging to the book. This is a required navigation property.
        /// </summary>
        virtual public List<Page> Pages { get; internal set; } = [];

        virtual public List<Event> Events { get; internal set; } = [];

        /// <summary>
        /// Returns the title of the book as a string representation.
        /// </summary>
        /// <returns>A string containing the title of the book.</returns>
        public override string ToString() => Title;
    }
}
