Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub TestMethod1()
        Dim value1 = "0011"
        Dim value2 = "00114"
        Dim value3 = "00111"
        Dim value4 = "0211"
        'Regex.IsMatch(value, "\w{1-35}")
        Debug.WriteLine(Regex.IsMatch(value1, "\b[01]{4,4}\b"))
        Debug.WriteLine(Regex.IsMatch(value2, "\b[01]{4,4}\b"))
        Debug.WriteLine(Regex.IsMatch(value2, "^[01]{4,4}$"))
        Debug.WriteLine(Regex.IsMatch(value3, "^[01]{4,4}$"))
        Debug.WriteLine(Regex.IsMatch(value4, "^[01]{4,4}$"))



    End Sub

End Class