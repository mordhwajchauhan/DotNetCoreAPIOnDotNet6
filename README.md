# DotNetCoreAPIOnDotNet6andAbove
asp.net core 8 jwt authentication - <b>OAuth 2.0 Client Credentials Grant</b>

<!-- wp:list -->
<ul><li>Visual Studio <strong>2022</strong></li><li>.NET Core <strong>8.0</strong><ul><li>If using <strong>Windows</strong> here is the guide on&nbsp; <a href="https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net80">how to install .net core on windows</a></li><li>If using <strong>Linux</strong> here is the guide on&nbsp; <a href="https://learn.microsoft.com/en-us/dotnet/core/install/linux">how to install .net core on ubuntu 22.04</a></li><li>If using <strong>Mac</strong> here is the easier way <a href="https://learn.microsoft.com/en-us/dotnet/core/install/macos">how to install .net core on mac</a></li></ul></li><li>Api testing tool <strong><a href="https://www.postman.com/">PostMan </a></strong>or we can use free tool <strong><a href="https://chrome.google.com/webstore/detail/talend-api-tester-free-ed/aejoelaoggembcahagimdiliamlcdmfm?hl=en">Talent API Tester Chrome plugin </a></strong> or inbuilt tool <strong><a href="https://swagger.io">Swagger</a></strong> can be used.</li><li>Nugget Packages used (pay attention to version numbers) </li></ul>
<!-- /wp:list -->

<!-- wp:quote {"align":"right","className":"is-style-default"} -->
<blockquote class="wp-block-quote has-text-align-right is-style-default"><p>Microsoft.AspNetCore.Authentication.JwtBearer&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Version <strong>8.0.0</strong></p><p>Microsoft.EntityFrameworkCore.InMemory&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;Version &nbsp;<strong>8.0.0</strong></p><p>AutoMapper.Extensions.Microsoft.DependencyInjection&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Version &nbsp;<strong>12.0.1</strong></p></blockquote>
<!-- /wp:quote -->

<!-- wp:block {"ref":1393} /-->

Prerequisite Knowledge 

<!-- wp:list -->
<ul><li>Do you know basics of Json Web Token? If not then Visit &nbsp;<a href="http://jwt.io/">jwt.io</a>&nbsp;for learning more about JWT . Chris has also provided good details about&nbsp;<a href="https://scotch.io/tutorials/the-anatomy-of-a-json-web-token">JWT info</a>.</li><li>Are you familiar about difference between authentication vs authorization here very brief write up on <a href="https://decatechlabs.com/difference-between-authentication-vs-authorization">this</a>?</li><li>Are your aware of filters concept in asp.net core ? Let’s have a brief intro on filters.</li></ul>
<!-- /wp:list -->
