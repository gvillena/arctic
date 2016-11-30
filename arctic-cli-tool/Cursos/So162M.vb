
Imports System.IO
Imports Newtonsoft.Json


Public Class So162M

    Public Shared Function GetStudents() As List(Of Student)

        Dim lst As List(Of Student) = Nothing
        Dim filepath As String = Path.Combine(My.Application.Info.DirectoryPath, "so162-lst-json.txt")
        Dim serializer As New JsonSerializer() With {.Formatting = Formatting.Indented}

        Using file As New StreamReader(filepath)
            lst = serializer.Deserialize(file, GetType(List(Of Student)))
            If lst IsNot Nothing Then
                Debug.WriteLine("Import from file succeeded: " & lst.Count)
            Else
                Debug.WriteLine("Import from file failed.")
            End If
        End Using

        Return lst
    End Function

End Class
