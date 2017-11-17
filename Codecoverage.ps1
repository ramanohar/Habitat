Param
(
    [parameter(Mandatory=$true)]
    [String[]]
    $buildSrcPath
)

$curdir = get-location

<#
$destination = New-Item "TestAssembilies" -type directory
#>
$var = get-childitem $curdir\* -include *Tests.dll -recurse
foreach ($file in $var)
 { 
	if($file.FullName -like '*\bin\debug\*')
	{
      $testassmbilies = $testassmbilies + $file.FullName +" " 
        write-host $file.FullName
	}
 }
	write-host "TestAssembilies file listing"
 
<#$destfiles = get-childitem $destination -Filter *Tests.dll
foreach ($file in  $destfiles)
 {  
	write-host $file.FullName
	$testassmbilies = $testassmbilies + $file.FullName +" " }
 #>
write-host $testassmbilies
write-host "******"
write-host "constructing cmd line string "
 
 $openCoverConsolePath = $buildSrcPath.TrimEnd("")+"\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe "  
 $registeargs= "-register:user -mergeoutput -mergebyhash -oldstyle "
 $filterargs = "-filter:+[*]* -[Fluent*]* -[xunit*]* -[Html*]* -[Lucene*]* -[netdumbster*]* -[*Tests]* "
 $outCoverfile = """-output:SitecoreDemoCoverage.xml"""  
 $xUnitTargetpath = """-target:"+$buildSrcPath+"\packages\xunit.runner.console.2.1.0\tools\xunit.console.exe""" 
 $xUnitTargetargs = """-targetargs:"+$testassmbilies.TrimEnd('') + " "+"-noshadow -xml SitecoreDemoxUnitResults.xml"
 $xUnitResultFile = """-xml SitecoreDemoxunitTests.xml"
 
 write-host  "*******"
 write-host $openCoverConsolePath
 write-host $registeargs
 write-host $filterargs
 write-host $outCoverfile 
 write-host $xUnitTargetpath 
 write-host $xUnitTargetargs 
 write-host $xUnitResultFile 
 write-host "********"
 
 write-host  "commandline String to Execute"
 $cmdline =  $openCoverConsolePath+$registeargs +""""+$filterargs+""""+" "+$outCoverfile+" "+$xUnitTargetpath+" "+ $xUnitTargetargs+""""
 
 <#+" "+$xUnitResultFile.TrimEnd(" ")+""""#>
 
 write-host $cmdline
<#& $cmdline#>
Set-Content -Path $buildSrcPath"\coverage.cmd" -Value $cmdline -Force 
write-host  "commandline String to Execute"

 
 