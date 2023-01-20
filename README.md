# School-Student OData service using SqlLite as database.

Here's the School sample data:

<img width="1553" alt="image" src="https://user-images.githubusercontent.com/9426627/213791389-7f13fa0d-4b6c-41cd-9f63-4ecace988528.png">

Here's Student sample data:

<img width="414" alt="image" src="https://user-images.githubusercontent.com/9426627/213791579-29189f9a-9e16-4c5f-b97b-3a23d4a98a90.png">


## Basic usages

Run the service and try any of these requests:

## Basic query:

http://localhost:5168/odata/$metadata

http://localhost:5168/$odata

http://localhost:5168/odata/schools?$filter=schoolid eq 3

http://localhost:5168/odata/schools?$filter=schoolid le 3

http://localhost:5168/odata/schools?$filter=schoolid ge 3

http://localhost:5168/odata/schools?$top=1

http://localhost:5168/odata/schools?$top=1&$skip=2

http://localhost:5168/odata/schools?$top=1&$orderby=schoolId desc

http://localhost:5168/odata/schools?$top=1&$orderby=mailaddress/AptNo

## $select:

http://localhost:5168/odata/schools?$top=1&$orderby=mailaddress/AptNo&$select=SchoolName

http://localhost:5168/odata/schools?$top=1&$orderby=mailaddress/AptNo&$select=mailaddress($select=street)

http://localhost:5168/odata/schools?$top=1&$orderby=mailaddress/AptNo&$select=schoolname,mailaddress($select=city,street)

## $expand:

http://localhost:5168/odata/schools?$top=1&$orderby=mailaddress/AptNo&$select=schoolname,mailaddress($select=city,street)&$expand=students

http://localhost:5168/odata/schools?$top=1&$orderby=mailaddress/AptNo&$select=schoolname,mailaddress($select=city,street)&$expand=students($top=1)

http://localhost:5168/odata/schools?$top=1&$orderby=mailaddress/AptNo&$select=schoolname,mailaddress($select=city,street)&$expand=students($top=1;$skip=1;$orderby=grade)

http://localhost:5168/odata/schools?$top=1&$orderby=mailaddress/AptNo&$select=schoolname,mailaddress($select=city,street)&$expand=students($top=1;$skip=1;$orderby=grade desc)

http://localhost:5168/odata/schools?$expand=students($select=FirstName;$filter=startswith(FirstName,'J'))&$select=SchoolName

## $compute:

http://localhost:5168/odata/students?$select=FirstName,LastName

http://localhost:5168/odata/students?$compute=concat(concat(FirstName,' '),LastName) as FullName&$select=FullName&$count=true

http://localhost:5168/odata/students?$compute=concat(concat(FirstName,' '),LastName) as FullName&$select=FullName&$filter=length(FullName) le 9&$count=true

## $apply:

http://localhost:5168/odata/students?$apply=groupby((FavoriteSport))

http://localhost:5168/odata/students?$apply=groupby((FavoriteSport), aggregate($count as count))

http://localhost:5168/odata/students?$apply=groupby((FavoriteSport), aggregate($count as count, Grade with average as Avg, Grade with sum as Total))

## $search
http://localhost:5168/odata/students?$filter=grade ge 90 and grade lt 95&$select=Grade

http://localhost:5168/odata/students?$search=A&$select=Grade

http://localhost:5168/odata/students?$search="A%2B"&$select=Grade

## More $filter

http://localhost:5168/odata/schools?$filter=ContactEmails/any(a: a eq 'help@mercury.com')
