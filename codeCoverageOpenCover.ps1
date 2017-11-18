Param
(
    [parameter(Mandatory=$true)]
    [String[]]
    $buildSrcPath,                # <--- Build Source Root Folder

    [parameter(Mandatory=$true)]
    [String[]]
    $codeCoverageOutputfile,      # <--- Code Coverage Output file ie. codeCoverageDotCover.xml (for SonarQube)

    [parameter(Mandatory=$true)]
    [String[]]
    $unitTestOutputfile,          # <--- Unit Test Result Output file ie. xUnitResults.xml (for SonarQube)

	[parameter(Mandatory=$true)]
    [String[]]
    $openCoverVersionFolder,      # <--- Code Coverage Output file ie. codeCoverageDotCover.xml (for SonarQube)

	[parameter(Mandatory=$true)]
    [String[]]
    $xUnitRunnerVersionFolder
)

$var = get-childitem $buildSrcPath\* -include *Tests.dll -recurse

write-host "**** - Fetching list of Test Assembilies.. Begin - **** "
foreach ($file in $var)
 { 
	<# Include assembilies within Bin\Debug folders from build locations #>
	if($file.FullName -like '*\bin\debug\*')
	{
      $testassmbilies = $testassmbilies + $file.FullName +" " 
	<# enable below statement to list of test assemblies for unit testing on build log #>	
        <# write-host $file.FullName #>
	}
 }
write-host "**** - Fetching list of Test Assembilies.. End - **** "

write-host $testassmbilies
write-host " **** - Construct Command Line for OpenCover Execution - ******"
 $openCoverConsolePath = $buildSrcPath.TrimEnd("")+"\packages\"+$openCoverVersionFolder+"\tools\OpenCover.Console.exe "  
 $registeargs= "-register:user -mergeoutput -mergebyhash -oldstyle "
 $filterargs = "-filter:+[*]* -[Fluent*]* -[xunit*]* -[Html*]* -[Lucene*]* -[netDumbster*]* -[*Tests]* "
 $outCoverfile = "-output: "+$codeCoverageOutputfile  
 $xUnitTargetpath = """-target:"+$buildSrcPath+"\packages\"+$xUnitRunnerVersionFolder+"\tools\xunit.console.exe""" 
 $xUnitTargetargs = """-targetargs:"+$testassmbilies.TrimEnd('') + " "+"-noshadow "+ "-xml "+$unitTestOutputfile
  
 write-host  "*******"
 write-host "OpenCoverconsolePath   : " $openCoverConsolePath
 write-host "Register Argument      : " $registeargs
 write-host "Filter Arguments       : " $filterargs
 write-host "OutputFile             : " $outCoverfile 
 write-host "xUnit Target Path      : " $xUnitTargetpath 
 write-host "xUnit Target Arguments : " $xUnitTargetargs 
 write-host "xUnit Result File      : " $xUnitResultFile 
 write-host "********"
 
 write-host  "commandline String to Execute"
 $cmdline =  $openCoverConsolePath+$registeargs +""""+$filterargs+""""+" "+""""+$outCoverfile+""""+" "+$xUnitTargetpath+" "+ $xUnitTargetargs+""""
 
<#& $cmdline#>
Set-Content -Path $buildSrcPath"\coverage.cmd" -Value $cmdline -Force 
write-host  "commandline String to Execute"
write-host $cmdline
cmd.exe /c $cmdline
 