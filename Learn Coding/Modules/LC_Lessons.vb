Imports System.Data.SQLite
Imports LCLib
Imports LCLib.Functions
Module LC_Lessons
    Public Fundamentals As New unit
    Public Java As New unit
    Public CPP As New unit
    Public HTML As New unit
    Public JavaScript As New unit
    Public PHP As New unit

    Public current_page As Integer = 0
    Public current_lesson As Integer = 0
    Public current_unit As unit

    Public lcdb_dir As String = base_dir & "lcdb\"
    Private LessonCss As String = System.IO.File.ReadAllText(base_dir & "\scripts\Lesson_Page.css")

    Public Language_Used As String
    Public Code_Edited As String
    Public ClassName_Edited As String

    Public Sub LoadLessons()
        LoadFundamentals()
        LoadJava()
        LoadCpp()
        LoadHTML()
        LoadJavaScript()
        LoadPHP()
    End Sub

    Public Sub LoadFundamentals()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u0-fundamentals.lcdb;Version=3;")
                cn.Open()
                Debug.Print("trying... {0}", cn)
                Debug.Print("File exist: fund db: {0}", IO.File.Exists(lcdb_dir & "u0-fundamentals.lcdb"))

                'Loads Unit Information
                Using cmd As New SQLiteCommand("SELECT * FROM unit_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            Fundamentals.Number = rs("unit_no")
                            Fundamentals.Language = rs("unit_lang")
                            Fundamentals.Title = rs("unit_title")
                        End While
                    End Using
                End Using
                'Loads Lessons Info
                Using cmd As New SQLiteCommand("SELECT * FROM lessons_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            Dim _lesson = New lesson
                            _lesson.Number = rs("lesson_no")
                            _lesson.Topic = rs("lesson_topic")
                            _lesson.Duration = rs("lesson_duration")
                            'Loads Lesson Pages
                            Fundamentals.Lessons.Add(_lesson)
                        End While
                    End Using
                End Using
                For Each _lesson In Fundamentals.Lessons
                    Using _cmd As New SQLiteCommand("SELECT * FROM lesson_" & _lesson.Number, cn)
                        Using _rs = _cmd.ExecuteReader
                            While _rs.Read
                                Dim _page = New page

                                Dim LessonHtml As New System.Text.StringBuilder
                                LessonHtml.AppendLine("<head><style>" & LessonCss & "</style></head>")
                                LessonHtml.AppendLine("<body><div id=""wrapper"">" & _rs("content") & "</div></body>")

                                _page.ID = Convert.ToInt32(_rs("ID"))
                                _page.Content = LessonHtml.ToString
                                _page.PageType = _rs("type")

                                Select Case _page.PageType
                                    Case "image"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "image_scroll"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "code"
                                        _page.MainLanguage = _rs("lang")
                                        _page.ClassName = _rs("class_name")
                                        _page.Codes.Java = _rs("java")
                                        _page.Codes.Cpp = _rs("cpp")
                                        _page.Codes.HTML = _rs("html")
                                        _page.Codes.JavaScript = _rs("js")
                                        _page.Codes.PHP = _rs("php")
                                    Case "video"
                                        _page.Links.Video = _rs("vid")
                                End Select
                                _page.Links.Source = _rs("src")
                                _lesson.Pages.Add(_page)
                            End While
                        End Using
                    End Using
                Next
            End Using
        Catch ex As Exception
            Debug.Print("Error: " & ex.Message)
            Debug.Print(lcdb_dir & "u0-fundamentals.lcdb;Version=3;")
            Debug.Print("HELL-O BROKE LOSE00")
            MsgBox("Database Error!")
            Environment.Exit(1)

        End Try
    End Sub
    Public Sub LoadJava()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u1-java.lcdb;Version=3;")
                cn.Open()
                'Loads Unit Information
                Using cmd As New SQLiteCommand("SELECT * FROM unit_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            Java.Number = rs("unit_no")
                            Java.Language = rs("unit_lang")
                            Java.Title = rs("unit_title")
                        End While
                    End Using
                End Using
                'Loads Lessons Info
                Using cmd As New SQLiteCommand("SELECT * FROM lessons_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            Dim _lesson = New lesson
                            _lesson.Number = rs("lesson_no")
                            _lesson.Topic = rs("lesson_topic")
                            _lesson.Duration = rs("lesson_duration")
                            'Loads Lesson Pages
                            Java.Lessons.Add(_lesson)
                        End While
                    End Using
                End Using
                For Each _lesson In Java.Lessons
                    Using _cmd As New SQLiteCommand("SELECT * FROM lesson_" & _lesson.Number, cn)
                        Using _rs = _cmd.ExecuteReader
                            While _rs.Read
                                Dim _page = New page

                                Dim LessonHtml As New System.Text.StringBuilder
                                LessonHtml.AppendLine("<head><style>" & LessonCss & "</style></head>")
                                LessonHtml.AppendLine("<body><div id=""wrapper"">" & _rs("content") & "</div></body>")

                                _page.ID = Convert.ToInt32(_rs("ID"))
                                _page.Content = LessonHtml.ToString
                                _page.PageType = _rs("type")

                                Select Case _page.PageType
                                    Case "image"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "image_scroll"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "code"
                                        _page.MainLanguage = _rs("lang")
                                        _page.ClassName = _rs("class_name")
                                        _page.Codes.Java = _rs("java")
                                        _page.Codes.Cpp = _rs("cpp")
                                        _page.Codes.HTML = _rs("html")
                                        _page.Codes.JavaScript = _rs("js")
                                        _page.Codes.PHP = _rs("php")
                                    Case "video"
                                        _page.Links.Video = _rs("vid")
                                End Select
                                _page.Links.Source = _rs("src")
                                _lesson.Pages.Add(_page)
                            End While
                        End Using
                    End Using
                Next
            End Using
        Catch ex As Exception
            Debug.Print("Error: " & ex.Message)
            Debug.Print(lcdb_dir & "u1-java.lcdb;Version=3;")
            Debug.Print("HELL-O BROKE LOSE01")
            MsgBox("Database Error!")
            Environment.Exit(1)
        End Try
    End Sub
    Public Sub LoadCpp()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u2-cpp.lcdb;Version=3;")
                cn.Open()
                'Loads Unit Information
                Using cmd As New SQLiteCommand("SELECT * FROM unit_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            CPP.Number = rs("unit_no")
                            CPP.Language = rs("unit_lang")
                            CPP.Title = rs("unit_title")
                        End While
                    End Using
                End Using
                'Loads Lessons Info
                Using cmd As New SQLiteCommand("SELECT * FROM lessons_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            Dim _lesson = New lesson
                            _lesson.Number = rs("lesson_no")
                            _lesson.Topic = rs("lesson_topic")
                            _lesson.Duration = rs("lesson_duration")
                            'Loads Lesson Pages
                            CPP.Lessons.Add(_lesson)
                        End While
                    End Using
                End Using
                For Each _lesson In CPP.Lessons
                    Using _cmd As New SQLiteCommand("SELECT * FROM lesson_" & _lesson.Number, cn)
                        Using _rs = _cmd.ExecuteReader
                            While _rs.Read
                                Dim _page = New page

                                Dim LessonHtml As New System.Text.StringBuilder
                                LessonHtml.AppendLine("<head><style>" & LessonCss & "</style></head>")
                                LessonHtml.AppendLine("<body><div id=""wrapper"">" & _rs("content") & "</div></body>")

                                _page.ID = Convert.ToInt32(_rs("ID"))
                                _page.Content = LessonHtml.ToString
                                _page.PageType = _rs("type")

                                Select Case _page.PageType
                                    Case "image"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "image_scroll"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "code"
                                        _page.MainLanguage = _rs("lang")
                                        _page.ClassName = _rs("class_name")
                                        _page.Codes.Java = _rs("java")
                                        _page.Codes.Cpp = _rs("cpp")
                                        _page.Codes.HTML = _rs("html")
                                        _page.Codes.JavaScript = _rs("js")
                                        _page.Codes.PHP = _rs("php")
                                    Case "video"
                                        _page.Links.Video = _rs("vid")
                                End Select
                                _page.Links.Source = _rs("src")
                                _lesson.Pages.Add(_page)
                            End While
                        End Using
                    End Using
                Next
            End Using
        Catch ex As Exception
            Debug.Print("Error: " & ex.Message)
            Debug.Print(lcdb_dir & "u2-cpp.lcdb;Version=3;")
            Debug.Print("HELL-O BROKE LOSE")
            MsgBox("Database Error!")
            Environment.Exit(1)
        End Try
    End Sub
    Public Sub LoadHTML()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u3-html.lcdb;Version=3;")
                cn.Open()
                'Loads Unit Information
                Using cmd As New SQLiteCommand("SELECT * FROM unit_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            HTML.Number = rs("unit_no")
                            HTML.Language = rs("unit_lang")
                            HTML.Title = rs("unit_title")
                        End While
                    End Using
                End Using
                'Loads Lessons Info
                Using cmd As New SQLiteCommand("SELECT * FROM lessons_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            Dim _lesson = New lesson
                            _lesson.Number = rs("lesson_no")
                            _lesson.Topic = rs("lesson_topic")
                            _lesson.Duration = rs("lesson_duration")
                            'Loads Lesson Pages
                            HTML.Lessons.Add(_lesson)
                        End While
                    End Using
                End Using
                For Each _lesson In HTML.Lessons
                    Using _cmd As New SQLiteCommand("SELECT * FROM lesson_" & _lesson.Number, cn)
                        Using _rs = _cmd.ExecuteReader
                            While _rs.Read
                                Dim _page = New page

                                Dim LessonHtml As New System.Text.StringBuilder
                                LessonHtml.AppendLine("<head><style>" & LessonCss & "</style></head>")
                                LessonHtml.AppendLine("<body><div id=""wrapper"">" & _rs("content") & "</div></body>")

                                _page.ID = Convert.ToInt32(_rs("ID"))
                                _page.Content = LessonHtml.ToString
                                _page.PageType = _rs("type")

                                Select Case _page.PageType
                                    Case "image"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "image_scroll"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "code"
                                        _page.MainLanguage = _rs("lang")
                                        _page.ClassName = _rs("class_name")
                                        _page.Codes.Java = _rs("java")
                                        _page.Codes.Cpp = _rs("cpp")
                                        _page.Codes.HTML = _rs("html")
                                        _page.Codes.JavaScript = _rs("js")
                                        _page.Codes.PHP = _rs("php")
                                    Case "video"
                                        _page.Links.Video = _rs("vid")
                                End Select
                                _page.Links.Source = _rs("src")
                                _lesson.Pages.Add(_page)
                            End While
                        End Using
                    End Using
                Next
            End Using
        Catch ex As Exception
            Debug.Print("Error: " & ex.Message)
            Debug.Print(lcdb_dir & "u3-html.lcdb;Version=3;")
            Debug.Print("HELL-O BROKE LOSE2")
            MsgBox("Database Error!")
            Environment.Exit(1)
        End Try
    End Sub
    Public Sub LoadJavaScript()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u4-js.lcdb;Version=3;")
                cn.Open()
                'Loads Unit Information
                Using cmd As New SQLiteCommand("SELECT * FROM unit_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            JavaScript.Number = rs("unit_no")
                            JavaScript.Language = rs("unit_lang")
                            JavaScript.Title = rs("unit_title")
                        End While
                    End Using
                End Using
                'Loads Lessons Info
                Using cmd As New SQLiteCommand("SELECT * FROM lessons_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            Dim _lesson = New lesson
                            _lesson.Number = rs("lesson_no")
                            _lesson.Topic = rs("lesson_topic")
                            _lesson.Duration = rs("lesson_duration")
                            'Loads Lesson Pages
                            JavaScript.Lessons.Add(_lesson)
                        End While
                    End Using
                End Using
                For Each _lesson In JavaScript.Lessons
                    Using _cmd As New SQLiteCommand("SELECT * FROM lesson_" & _lesson.Number, cn)
                        Using _rs = _cmd.ExecuteReader
                            While _rs.Read
                                Dim _page = New page

                                Dim LessonHtml As New System.Text.StringBuilder
                                LessonHtml.AppendLine("<head><style>" & LessonCss & "</style></head>")
                                LessonHtml.AppendLine("<body><div id=""wrapper"">" & _rs("content") & "</div></body>")

                                _page.ID = Convert.ToInt32(_rs("ID"))
                                _page.Content = LessonHtml.ToString
                                _page.PageType = _rs("type")

                                Select Case _page.PageType
                                    Case "image"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "image_scroll"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "code"
                                        _page.MainLanguage = _rs("lang")
                                        _page.ClassName = _rs("class_name")
                                        _page.Codes.Java = _rs("java")
                                        _page.Codes.Cpp = _rs("cpp")
                                        _page.Codes.HTML = _rs("html")
                                        _page.Codes.JavaScript = _rs("js")
                                        _page.Codes.PHP = _rs("php")
                                    Case "video"
                                        _page.Links.Video = _rs("vid")
                                End Select
                                _page.Links.Source = _rs("src")
                                _lesson.Pages.Add(_page)
                            End While
                        End Using
                    End Using
                Next
            End Using
        Catch ex As Exception
            Debug.Print("Error: " & ex.Message)
            Debug.Print(lcdb_dir & "u4-js.lcdb;Version=3;")
            Debug.Print("HELL-O BROKE LOSE3")
            MsgBox("Database Error!")
            Environment.Exit(1)
        End Try
    End Sub
    Public Sub LoadPHP()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u5-php.lcdb;Version=3;")
                cn.Open()
                'Loads Unit Information
                Using cmd As New SQLiteCommand("SELECT * FROM unit_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            PHP.Number = rs("unit_no")
                            PHP.Language = rs("unit_lang")
                            PHP.Title = rs("unit_title")
                        End While
                    End Using
                End Using
                'Loads Lessons Info
                Using cmd As New SQLiteCommand("SELECT * FROM lessons_info", cn)
                    Using rs = cmd.ExecuteReader
                        While rs.Read
                            Dim _lesson = New lesson
                            _lesson.Number = rs("lesson_no")
                            _lesson.Topic = rs("lesson_topic")
                            _lesson.Duration = rs("lesson_duration")
                            'Loads Lesson Pages
                            PHP.Lessons.Add(_lesson)
                        End While
                    End Using
                End Using
                For Each _lesson In PHP.Lessons
                    Using _cmd As New SQLiteCommand("SELECT * FROM lesson_" & _lesson.Number, cn)
                        Using _rs = _cmd.ExecuteReader
                            While _rs.Read
                                Dim _page = New page

                                Dim LessonHtml As New System.Text.StringBuilder
                                LessonHtml.AppendLine("<head><style>" & LessonCss & "</style></head>")
                                LessonHtml.AppendLine("<body><div id=""wrapper"">" & _rs("content") & "</div></body>")

                                _page.ID = Convert.ToInt32(_rs("ID"))
                                _page.Content = LessonHtml.ToString
                                _page.PageType = _rs("type")

                                Select Case _page.PageType
                                    Case "image"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "image_scroll"
                                        Dim img_bytes() As Byte = _rs("img")
                                        _page.Image = img_bytes.ByteToImage
                                    Case "code"
                                        _page.MainLanguage = _rs("lang")
                                        _page.ClassName = _rs("class_name")
                                        _page.Codes.Java = _rs("java")
                                        _page.Codes.Cpp = _rs("cpp")
                                        _page.Codes.HTML = _rs("html")
                                        _page.Codes.JavaScript = _rs("js")
                                        _page.Codes.PHP = _rs("php")
                                    Case "video"
                                        _page.Links.Video = _rs("vid")
                                End Select
                                _page.Links.Source = _rs("src")
                                _lesson.Pages.Add(_page)
                            End While
                        End Using
                    End Using
                Next
            End Using
        Catch ex As Exception
            Debug.Print("Error: " & ex.Message)
            Debug.Print(lcdb_dir & "u5-php.lcdb;Version=3;")
            Debug.Print("HELL-O BROKE LOSE5")
            MsgBox("Database Error!")
            Environment.Exit(1)
        End Try
    End Sub
End Module
