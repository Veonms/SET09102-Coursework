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
        public void GetHashtagList_NoHashtags_ReturnsEmptyDictionary()
        {
            var mfNoHashtags = new MessageFacade();

            mfNoHashtags.AddMessage("T123456789", "@john Everyone welcome Peter to the company");

            var list = mfNoHashtags.GetHashtagList();

            Assert.IsTrue(list.Values.Count == 0);
        }

        [TestMethod]
        public void GetSIR_SIRExists_ReturnsSIR()
        {
            var mfSIRExists = new MessageFacade();


            var result = mfSIRExists.GetSIR("SIR 20/08/2020 Sort Code: 24-25-63 Nature of Incident: Theft Hello John.Theres been a break in. Can you look at the CCTV. Thanks");

            Assert.AreEqual(result, "Sort Code: 24-25-63\nNature of Incident: Theft");
        }

        [TestMethod]
        public void GetSIR_NoSIRs_ReturnsEmptyString()
        {
            var mfSIRExists = new MessageFacade();

            var result = mfSIRExists.GetSIR("Hi Peter. Can you forward the minutes from todays meeting. Thanks");

            Assert.AreEqual(result, "");
        }

        [TestMethod]
        public void CheckURL_URLExists_ReturnsMessageWithURLQuarantined()
        {
            var mfCheckURLUrlExists = new MessageFacade();

            var result = mfCheckURLUrlExists.checkURL("Dave check out this site https://google.com");

            Assert.AreEqual(result, "Dave check out this site <URL Quarantined>");
        }

        [TestMethod]
        public void CheckURL_NoURLExists_ReturnsBodyUnchanged()
        {
            var mfCheckURLNoUrlExists = new MessageFacade();

            var result = mfCheckURLNoUrlExists.checkURL("Dave can you send me this weeks rota");

            Assert.AreEqual(result, "Dave can you send me this weeks rota");
        }

        [TestMethod]
        public void GetURL_URLExists_ReturnsList()
        {
            var mfGetURLUrlExists = new MessageFacade();

            var result = mfGetURLUrlExists.GetUrl("Dave check out this site https://google.com");

            Assert.IsTrue(result.Count == 1 && result.Contains(" https://google.com"));
        }

        [TestMethod]
        public void GetURL_noURLExists_ReturnsEmptyList()
        {
            var mfGetURLNonEExist = new MessageFacade();

            var result = mfGetURLNonEExist.GetUrl("Dave can you send me this weeks rota");

            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetMentions_MentionsExist_ReturnsList()
        {
            var mfGetMentionsMentionsExist = new MessageFacade();

            var result = mfGetMentionsMentionsExist.GetMentions("@John Hey @Dave. Can you send me the rota");

            Assert.IsTrue(result.Count ==1 && result.Contains("@Dave"));
        }

        [TestMethod]
        public void GetMentions_NoMentionsExist_ReturnsEmptyList()
        {
            var mfGetMentionsNoMentionsExist = new MessageFacade();

            var result = mfGetMentionsNoMentionsExist.GetMentions("@John Hey Dav can you send me the rota");
            
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetHashtag_HashtagExists_ReturnsList()
        {
            var mfGetHashtagHashtagExists = new MessageFacade();

            var result = mfGetHashtagHashtagExists.GetHashtag("@John Hey Dav can you send me the #rota ");

            Assert.IsTrue(result.Contains("#rota") && result.Count == 1);
        }

        [TestMethod]
        public void GetHashtag_NoHashtagExists_ReturnsEmptyList()
        {
            var mfGetHashtagNoHashtagExists = new MessageFacade();

            var result = mfGetHashtagNoHashtagExists.GetHashtag("@John Hey Dave can you send me the rota");

            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void DisplayData_MessageeObjectsExist_ReturnsListOfHeaderAndBody()
        {
            var mfDisplayData = new MessageFacade();

            mfDisplayData.AddMessage("S123456789","Hello David");
            mfDisplayData.AddMessage("T123456789", "@ John Hello David");

            var result = mfDisplayData.DisplayData();

            Assert.IsTrue(result.Count == 4);
        }
    }
}
