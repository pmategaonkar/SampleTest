# SampleTest
This project has 3 layers:
<ul>
<li> Console application, consumer </li>
<li> Business layer, orchastration service </li>
<li> Data layer, service responsible for fetching data</li>

## Brief of handling following points/how to achieve:
<ol>
<li>The data source may change.</li>
    Change the baseuri or method or both for any changes in data source.
<li>The endpoint could go down.</li>
    User will get a proper message that service is unable to fetch the data.
<li>The endpoint has known to be slow in the past.</li>
    Timeout to fetch the data is set to 5 seconds, can be changed easily in DataService.cs
<li>The way source is fetched may change.</li>
    Logic to fetch the data is hidden from consumer, as long interface is unchanged, any consumer need not to change any code.
<li>The number of records may change (performance).</li>
    Caching has been implemented to reduce the number of calls to service, also user has option to invalidate cache and fetch records from service.
<li>The functionality may not always be consumed in a console app.</li>
    Service funcationality has been exposed via interface, hence consumer can be any type of application.
