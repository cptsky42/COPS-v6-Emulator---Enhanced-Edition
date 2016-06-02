// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2010 - 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Collections.Generic;

namespace COServer
{
    /// <summary>
    /// Represent a message board.
    /// 
    /// The class is thread-safe.
    /// </summary>
    public class MessageBoard
    {
        /// <summary>
        /// The number of characters of the message to show in the titles.
        /// </summary>
        private const Int32 TITLE_SIZE = 44;
        /// <summary>
        /// The number of messages to list on a page.
        /// </summary>
        private const Int32 LIST_SIZE = 10;

        /// <summary>
        /// The information of a message.
        /// </summary>
        public struct MessageInfo
        {
            /// <summary>
            /// Author of the message.
            /// </summary>
            public String Author;
            /// <summary>
            /// Content of the message.
            /// </summary>
            public String Words;
            /// <summary>
            /// Date & time of when the message was posted.
            /// It is represented in the yyyyMMddHHmmss format.
            /// </summary>
            public String Date;
        };

        /// <summary>
        /// All the messages of the board.
        /// </summary>
        private readonly List<MessageInfo> mMessages = new List<MessageInfo>();

        /// <summary>
        /// Add a new message to the board.
        /// </summary>
        /// <param name="aAuthor">Author of the message.</param>
        /// <param name="aWords">Content of the message.</param>
        public void Add(String aAuthor, String aWords)
        {
            MessageInfo message = new MessageInfo();
            message.Author = aAuthor;
            message.Words = aWords;
            message.Date = DateTime.Now.ToString("yyyyMMddHHmmss");

            lock (mMessages)
            {
                mMessages.Add(message);
            }
        }

        /// <summary>
        /// Delete the message from the board.
        /// </summary>
        /// <param name="aMessage">Message to delete.</param>
        public void Delete(MessageInfo aMessage)
        {
            lock (mMessages)
            {
                if (mMessages.Contains(aMessage))
                    mMessages.Remove(aMessage);
            }
        }

        /// <summary>
        /// Get the list of messages for the specified page.
        /// </summary>
        /// <param name="aIndex">Index (page) on the board</param>
        /// <returns>An array of strings containing the author, the title and the date.</returns>
        public String[] GetList(UInt16 aIndex)
        {
            String[] list = null;

            lock (mMessages)
            {
                if (mMessages.Count == 0)
                    return null;

                if ((aIndex / 8 * LIST_SIZE) > mMessages.Count)
                    return null;

                Int32 start = (mMessages.Count - ((aIndex / 8 * LIST_SIZE) + 1));

                if (start < LIST_SIZE)
                    list = new String[(start + 1) * 3];
                else
                    list = new String[LIST_SIZE * 3];

                Int32 end = (start - (list.Length / 3));

                Int32 x = 0;
                for (Int32 i = start; i > end; i--)
                {
                    MessageInfo message = mMessages[i];
                    list[x + 0] = message.Author;
                    if (message.Words.Length > TITLE_SIZE)
                        list[x + 1] = message.Words.Remove(TITLE_SIZE, message.Words.Length - TITLE_SIZE);
                    else
                        list[x + 1] = message.Words;
                    list[x + 2] = message.Date;
                    x += 3;
                }
            }

            return list;
        }

        /// <summary>
        /// Get the content of the message.
        /// </summary>
        /// <param name="aAuthor">Author of the message to retrieve.</param>
        /// <returns>The content of the message.</returns>
        public String GetWords(String aAuthor)
        {
            lock (mMessages)
            {
                foreach (MessageInfo message in mMessages)
                {
                    if (message.Author == aAuthor)
                        return message.Words;
                }
            }

            return "";
        }

        /// <summary>
        /// Get the message information by the author.
        /// </summary>
        /// <param name="aAuthor">Author of the message to retrieve.</param>
        /// <returns>The message information (author, content, date).</returns>
        public MessageInfo GetMsgInfoByAuthor(String aAuthor)
        {
            lock (mMessages)
            {
                foreach (MessageInfo message in mMessages)
                {
                    if (message.Author == aAuthor)
                        return message;
                }
            }

            return new MessageInfo();
        }
    }
}
