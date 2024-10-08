﻿// This file is part of VirtualStorageLibrary.
//
// Copyright (C) 2024 Akira Shimodate
//
// VirtualStorageLibrary is free software, and it is distributed under the terms of 
// the GNU Lesser General Public License (version 3, or at your option, any later 
// version). This license is published by the Free Software Foundation.
//
// VirtualStorageLibrary is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY, without even the implied warranty of MERCHANTABILITY or 
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for 
// more details.
//
// You should have received a copy of the GNU Lesser General Public License along 
// with VirtualStorageLibrary. If not, see https://www.gnu.org/licenses/.

namespace AkiraNetwork.VirtualStorageLibrary
{
    /// <summary>
    /// Represents the conditions for sorting data, holding the property to sort by and the order (ascending or descending).
    /// </summary>
    /// <typeparam name="T">The type of the entity to be sorted.</typeparam>
    public class VirtualSortCondition<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualSortCondition{T}"/> class with the specified sorting property and order.
        /// </summary>
        /// <param name="sortBy">The property to sort by.</param>
        /// <param name="ascending">A value indicating whether the sorting order is ascending.</param>
        public VirtualSortCondition(Expression<Func<T, object>> sortBy, bool ascending = true)
        {
            SortBy = sortBy;
            Ascending = ascending;
        }

        /// <summary>
        /// Gets or sets the property used for sorting.
        /// </summary>
        /// <value>
        /// An expression that specifies the property to use for sorting.
        /// </value>
        public Expression<Func<T, object>> SortBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sorting order is ascending.
        /// True if the order is ascending; otherwise, false.
        /// </summary>
        /// <value>
        /// A boolean value indicating whether the sorting order is ascending.
        /// </value>
        public bool Ascending { get; set; }
    }
}
