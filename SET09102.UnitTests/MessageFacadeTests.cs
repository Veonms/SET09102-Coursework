using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;

namespace SET09102.UnitTests
{
    [TestClass]
    public class MessageFacadeTests
    {
        [TestMethod]
        public void GetURLList_URLsExist_ReturnsURL()
        {
            var mfURLExisit = new MessageFacade();

            mfURLExisit.AddMessage("E123456789", "https://google.com");

            var list = mfURLExisit.GetURLList();

            Assert.IsTrue(list.Contains("https://google.com"));
        }

        [TestMethod]
        public void GetURLList_NoURLs_ReturnsEmptyList()
        {
            var mfNoURLs = new MessageFacade();

            mfNoURLs.AddMessage("E123456789", "Hello Dave. Hope all is well");

            var list = mfNoURLs.GetURLList();

            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void GetSIRList_SIRExists_ReturnsList()
        {
            var mfSIRExists = new MessageFacade();

            mfSIRExists.AddMessage("E123456789", "SIR 20/08/2020 Sort Code: 24-25-63 Nature of Incident: Theft Hello John. Theres been a break in. Can you look at the CCTV. Thanks");

            var list = mfSIRExists.GetSIRList();

            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void GetSIRList_NoSIRs_ReturnsEmptyList()
        {
            var mfSIRExists = new MessageFacade();

            mfSIRExists.AddMessage("E123456789", "Hi Peter. Can you forward the minutes from todays meeting. Thanks");

            var list = mfSIRExists.GetSIRList();

            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void GetMentionsList_MentionsExists_ReturnsList()
        {
            var mfMentionsExist = new MessageFacade();

            mfMentionsExist.AddMessage("T123456789", "@john Everyone welcome @Peter to the company");

            var list = mfMentionsExist.GetMentionsList();

            Assert.IsTrue(list.Count == 1 && list.Contains("@Peter"));
        }

        [TestMethod]
        public void GetMentionsList_NoMentions_ReturnsEmptyList()
        {
            var mfMentionsExist = new MessageFacade();

            mfMentionsExist.AddMessage("T123456789", "@john Everyone welcome Peter to the company");

            var list = mfMentionsExist.GetMentionsList();

            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void GetHashtagList_HashtagExists_ReturnsList()
        {
            var mfHashtagExists = new MessageFacade();

            mfHashtagExists.AddMessage("T123456789", "@john Everyone welcome Peter to the company #Party #Party #Welcome ");

            var list = mfHashtagExists.GetHashtagList();

            Assert.IsTrue(list.ContainsKey("#Party") && list["#Party"] == 2 &&
                list.ContainsKey("#Welcome") && list["#Welcome"] == 1);
        }

        [TestMethod]
        public void GetMentionsList_NoHashtags_ReturnsEmptyDictionary()
        {
            var mfNoHashtags = new MessageFacade();

            mfNoHashtags.AddMessage("T123456789", "@john Everyone welcome Peter to the company");

            var list = mfNoHashtags.GetHashtagList();

            Assert.IsTrue(list.Count == 0);
        }
        
        [TestMethod]
        public void GetSir_SIRExists_ReturnsSIR()
        {
            var mfSIRExists = new MessageFacade();


            var result = mfSIRExists.GetSIR("SIR 20/08/2020 Sort Code: 24-25-63 Nature of Incident: Theft Hello John.Theres been a break in. Can you look at the CCTV. Thanks");

            Assert.AreEqual(result, "Sort Code: 24-25-63\nNature of Incident: Theft");
        }
    }
}
