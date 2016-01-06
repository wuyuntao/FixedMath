$SolutionDir = "$PSScriptRoot\.."

$Runner = "$SolutionDir\packages\NUnit.Console.3.0.1\tools\nunit3-console.exe"

$TestExe = "$SolutionDir\FixedMathTests\bin\Debug\FixedMathTests.exe"

& $Runner $TestExe
