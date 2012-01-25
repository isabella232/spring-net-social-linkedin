﻿#region License

/*
 * Copyright 2002-2012 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System.Net;
using System.Collections.Generic;

using NUnit.Framework;
using Spring.Rest.Client.Testing;

using Spring.Http;
using Spring.IO;

namespace Spring.Social.LinkedIn.Api.Impl
{
    /// <summary>
    /// Unit tests for the ProfileTemplate class.
    /// </summary>
    /// <author>Bruno Baia</author>
    [TestFixture]
    public class ProfileTemplateTests : AbstractLinkedInOperationsTests 
    {    
	    [Test]
	    public void GetUserProfile()
        {
		    mockServer.ExpectNewRequest()
                .AndExpect(UriStartsWith("https://api.linkedin.com/v1/people/~:("))
				.AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
		    LinkedInProfile profile = linkedIn.ProfileOperations.GetUserProfileAsync().Result;
#else
            LinkedInProfile profile = linkedIn.ProfileOperations.GetUserProfile();
#endif
            AssertProfile(profile, "xO3SEJSVZN", "Architecte en informatique spécialisé sur les technologies Microsoft .NET",
                "Bruno", "Baia", "Information Technology and Services", "http://media.linkedin.com/pictureUrl", 
                "Consultant .NET indépendant", "http://www.linkedin.com/in/bbaia");
	    }

        [Test]
        public void GetUserProfileById()
        {
            mockServer.ExpectNewRequest()
                .AndExpect(UriStartsWith("https://api.linkedin.com/v1/people/id=xO3SEJSVZN:("))
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            LinkedInProfile profile = linkedIn.ProfileOperations.GetUserProfileByIdAsync("xO3SEJSVZN").Result;
#else
            LinkedInProfile profile = linkedIn.ProfileOperations.GetUserProfileById("xO3SEJSVZN");
#endif
            AssertProfile(profile, "xO3SEJSVZN", "Architecte en informatique spécialisé sur les technologies Microsoft .NET",
                "Bruno", "Baia", "Information Technology and Services", "http://media.linkedin.com/pictureUrl",
                "Consultant .NET indépendant", "http://www.linkedin.com/in/bbaia");
        }

        [Test]
        public void GetUserProfileByPublicUrl()
        {
            mockServer.ExpectNewRequest()
                .AndExpect(UriStartsWith("https://api.linkedin.com/v1/people/url=http://www.linkedin.com/in/bbaia:("))
                .AndExpectMethod(HttpMethod.GET)
                .AndRespondWith(JsonResource("Profile"), responseHeaders);

#if NET_4_0 || SILVERLIGHT_5
            LinkedInProfile profile = linkedIn.ProfileOperations.GetUserProfileByPublicUrlAsync("http://www.linkedin.com/in/bbaia").Result;
#else
            LinkedInProfile profile = linkedIn.ProfileOperations.GetUserProfileByPublicUrl("http://www.linkedin.com/in/bbaia");
#endif
            AssertProfile(profile, "xO3SEJSVZN", "Architecte en informatique spécialisé sur les technologies Microsoft .NET",
                "Bruno", "Baia", "Information Technology and Services", "http://media.linkedin.com/pictureUrl",
                "Consultant .NET indépendant", "http://www.linkedin.com/in/bbaia");
        }
    }
}
