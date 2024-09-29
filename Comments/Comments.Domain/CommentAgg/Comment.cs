﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace Comments.Domain.CommentAgg
{
	public class Comment : BaseEntity<long>
	{

		public long UserId { get; private  set; }

		public long OwnerId { get; private set; }

		public string FullName { get; private set; }

		public string? Email { get; private set; }

		public string Text { get; private set; }

		public CommentStatus Status { get; private set; }

		public CommentFor CommentFor { get; private set; }

		public string? WhyRejected { get; private set; }

		public long ParentId { get; private set; }

		public Comment? ParentComment { get; private set; }

		public List<Comment> ChildComments { get; private set; }

		public Comment()
		{
			ChildComments = new();
			ParentComment = null;
		}

		public Comment(long userId, long ownerId, string fullName, string? email, string text, CommentFor commentFor, long parentId = 0)
		{
			UserId = userId;
			OwnerId = ownerId;
			FullName = fullName;
			Email = email;
			Text = text;
			CommentFor = commentFor;
			ParentId = parentId;
			Status = CommentStatus.Not_Seen_Yet;
			CommentFor = commentFor;
		}

		public void RejectedComment(string why)
		{
			Status = CommentStatus.Rejected;
			WhyRejected = why;
		}
		public void AcceptedComment()
		{
			Status = CommentStatus.Approved;
		}
	}
}