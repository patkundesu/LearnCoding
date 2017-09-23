Imports LCLib
Imports System.Data.SQLite
Module LC_Test
    Dim TestCss As String = System.IO.File.ReadAllText(base_dir & "\scripts\Lesson_Test.css")

    Public _test As test

    Public current_test As test
    Public current_item As Integer

    Public score As Integer
    Public percentage As Double

    Public total_score As Integer
    Public total_percentage As Double

    Public _question As question
    Public Sub LoadTests()
        LoadFundamentalsTest()
        LoadJavaTest()
        LoadCppTest()
        LoadHtmlTest()
        LoadJavaScriptTest()
        LoadPHPTest()
    End Sub
    Public Sub LoadFundamentalsTest()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u0-tests.lcdb;Version=3;")
                cn.Open()
                Using cmd As New SQLiteCommand("SELECT Count(*) FROM sqlite_master WHERE type='table'", cn)
                    For i = 1 To Convert.ToInt32(cmd.ExecuteScalar()) - 1
                        _test = New test
                        _test.Number = i
                        Using acmd As New SQLiteCommand("SELECT * FROM l" & i & "_test", cn)
                            Using rs = acmd.ExecuteReader
                                While rs.Read
                                    _question = New question
                                    Dim TestHtml = New System.Text.StringBuilder
                                    TestHtml.AppendLine("<head><style>" & TestCss & "</style></head>")
                                    TestHtml.AppendLine("<body><div id=""wrapper"">" & rs("given") & "</div></body>")

                                    _question.ID = Convert.ToInt32(rs("ID"))
                                    _question.Given = TestHtml.ToString
                                    _question.Type = rs("type")
                                    _question.Language = rs("lang_foc")
                                    _question.Code = rs("code")
                                    Dim _choices() = rs("choices").ToString.Split("\")
                                    _question.Choices.AddRange(_choices)
                                    _question.Answer = rs("answer")
                                    _question.Misc.EssayLimit = Convert.ToInt32(rs("es_limit"))
                                    Dim _keywords() = rs("id_key").ToString.Split("\")
                                    _question.Misc.IdentificationKeywords.AddRange(_keywords)
                                    _test.Questions.Add(_question)
                                End While
                            End Using
                        End Using
                        Fundamentals.Lessons(i - 1).Test = _test
                    Next
                End Using
            End Using
        Catch ex As Exception
            MsgBox("HELL-O_BROKE_LOOSE_D:")
        End Try

    End Sub
    Public Sub LoadJavaTest()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u1-tests.lcdb;Version=3;")
                cn.Open()
                Using cmd As New SQLiteCommand("SELECT Count(*) FROM sqlite_master WHERE type='table'", cn)
                    For i = 1 To Convert.ToInt32(cmd.ExecuteScalar()) - 1
                        _test = New test
                        _test.Number = i
                        Using acmd As New SQLiteCommand("SELECT * FROM l" & i & "_test", cn)
                            Using rs = acmd.ExecuteReader
                                While rs.Read
                                    _question = New question
                                    Dim TestHtml = New System.Text.StringBuilder
                                    TestHtml.AppendLine("<head><style>" & TestCss & "</style></head>")
                                    TestHtml.AppendLine("<body><div id=""wrapper"">" & rs("given") & "</div></body>")

                                    _question.ID = Convert.ToInt32(rs("ID"))
                                    _question.Given = TestHtml.ToString
                                    _question.Type = rs("type")
                                    _question.Language = rs("lang_foc")
                                    _question.ClassName = rs("class_name")
                                    _question.Tries = Convert.ToInt32(rs("tries"))
                                    _question.Code = rs("code")
                                    Dim _choices() = rs("choices").ToString.Split("\")
                                    _question.Choices.AddRange(_choices)
                                    _question.Answer = rs("answer")
                                    _question.Misc.EssayLimit = Convert.ToInt32(rs("es_limit"))
                                    Dim _keywords() = rs("id_key").ToString.Split("\")
                                    _question.Misc.IdentificationKeywords.AddRange(_keywords)
                                    Dim _inputs() = rs("cd_inputs").ToString.Split("\")
                                    _question.Misc.CodingInputs.AddRange(_inputs)
                                    Dim _outputs() = rs("cd_outputs").ToString.Split("\")
                                    _question.Misc.CodingOutputs.AddRange(_outputs)
                                    _test.Questions.Add(_question)
                                End While
                            End Using
                        End Using
                        Java.Lessons(i - 1).Test = _test
                    Next
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LoadCppTest()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u2-tests.lcdb;Version=3;")
                cn.Open()
                Using cmd As New SQLiteCommand("SELECT Count(*) FROM sqlite_master WHERE type='table'", cn)
                    For i = 1 To Convert.ToInt32(cmd.ExecuteScalar()) - 1
                        _test = New test
                        _test.Number = i
                        Using acmd As New SQLiteCommand("SELECT * FROM l" & i & "_test", cn)
                            Using rs = acmd.ExecuteReader
                                While rs.Read
                                    _question = New question
                                    Dim TestHtml = New System.Text.StringBuilder
                                    TestHtml.AppendLine("<head><style>" & TestCss & "</style></head>")
                                    TestHtml.AppendLine("<body><div id=""wrapper"">" & rs("given") & "</div></body>")

                                    _question.ID = Convert.ToInt32(rs("ID"))
                                    _question.Given = TestHtml.ToString
                                    _question.Type = rs("type")
                                    _question.Language = rs("lang_foc")
                                    _question.ClassName = rs("class_name")
                                    _question.Tries = Convert.ToInt32(rs("tries"))
                                    _question.Code = rs("code")
                                    Dim _choices() = rs("choices").ToString.Split("\")
                                    _question.Choices.AddRange(_choices)
                                    _question.Answer = rs("answer")
                                    _question.Misc.EssayLimit = Convert.ToInt32(rs("es_limit"))
                                    Dim _keywords() = rs("id_key").ToString.Split("\")
                                    _question.Misc.IdentificationKeywords.AddRange(_keywords)
                                    Dim _inputs() = rs("cd_inputs").ToString.Split("\")
                                    _question.Misc.CodingInputs.AddRange(_inputs)
                                    Dim _outputs() = rs("cd_outputs").ToString.Split("\")
                                    _question.Misc.CodingOutputs.AddRange(_outputs)
                                    _test.Questions.Add(_question)
                                End While
                            End Using
                        End Using
                        CPP.Lessons(i - 1).Test = _test
                    Next
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LoadHtmlTest()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u3-tests.lcdb;Version=3;")
                cn.Open()
                Using cmd As New SQLiteCommand("SELECT Count(*) FROM sqlite_master WHERE type='table'", cn)
                    For i = 1 To Convert.ToInt32(cmd.ExecuteScalar()) - 1
                        _test = New test
                        _test.Number = i
                        Using acmd As New SQLiteCommand("SELECT * FROM l" & i & "_test", cn)
                            Using rs = acmd.ExecuteReader
                                While rs.Read
                                    _question = New question
                                    Dim TestHtml = New System.Text.StringBuilder
                                    TestHtml.AppendLine("<head><style>" & TestCss & "</style></head>")
                                    TestHtml.AppendLine("<body><div id=""wrapper"">" & rs("given") & "</div></body>")

                                    _question.ID = Convert.ToInt32(rs("ID"))
                                    _question.Given = TestHtml.ToString
                                    _question.Type = rs("type")
                                    _question.Language = rs("lang_foc")
                                    _question.ClassName = rs("class_name")
                                    _question.Tries = Convert.ToInt32(rs("tries"))
                                    _question.Code = rs("code")
                                    Dim _choices() = rs("choices").ToString.Split("\")
                                    _question.Choices.AddRange(_choices)
                                    _question.Answer = rs("answer")
                                    _question.Misc.EssayLimit = Convert.ToInt32(rs("es_limit"))
                                    Dim _keywords() = rs("id_key").ToString.Split("\")
                                    _question.Misc.IdentificationKeywords.AddRange(_keywords)
                                    Dim _inputs() = rs("cd_inputs").ToString.Split("\")
                                    _question.Misc.CodingInputs.AddRange(_inputs)
                                    Dim _outputs() = rs("cd_outputs").ToString.Split("\")
                                    _question.Misc.CodingOutputs.AddRange(_outputs)
                                    _test.Questions.Add(_question)
                                End While
                            End Using
                        End Using
                        HTML.Lessons(i - 1).Test = _test
                    Next
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LoadJavaScriptTest()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u4-tests.lcdb;Version=3;")
                cn.Open()
                Using cmd As New SQLiteCommand("SELECT Count(*) FROM sqlite_master WHERE type='table'", cn)
                    For i = 1 To Convert.ToInt32(cmd.ExecuteScalar()) - 1
                        _test = New test
                        _test.Number = i
                        Using acmd As New SQLiteCommand("SELECT * FROM l" & i & "_test", cn)
                            Using rs = acmd.ExecuteReader
                                While rs.Read
                                    _question = New question
                                    Dim TestHtml = New System.Text.StringBuilder
                                    TestHtml.AppendLine("<head><style>" & TestCss & "</style></head>")
                                    TestHtml.AppendLine("<body><div id=""wrapper"">" & rs("given") & "</div></body>")

                                    _question.ID = Convert.ToInt32(rs("ID"))
                                    _question.Given = TestHtml.ToString
                                    _question.Type = rs("type")
                                    _question.Language = rs("lang_foc")
                                    _question.ClassName = rs("class_name")
                                    _question.Tries = Convert.ToInt32(rs("tries"))
                                    _question.Code = rs("code")
                                    Dim _choices() = rs("choices").ToString.Split("\")
                                    _question.Choices.AddRange(_choices)
                                    _question.Answer = rs("answer")
                                    _question.Misc.EssayLimit = Convert.ToInt32(rs("es_limit"))
                                    Dim _keywords() = rs("id_key").ToString.Split("\")
                                    _question.Misc.IdentificationKeywords.AddRange(_keywords)
                                    Dim _inputs() = rs("cd_inputs").ToString.Split("\")
                                    _question.Misc.CodingInputs.AddRange(_inputs)
                                    Dim _outputs() = rs("cd_outputs").ToString.Split("\")
                                    _question.Misc.CodingOutputs.AddRange(_outputs)
                                    _test.Questions.Add(_question)
                                End While
                            End Using
                        End Using
                        JavaScript.Lessons(i - 1).Test = _test
                    Next
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Public Sub LoadPHPTest()
        Try
            Using cn As New SQLiteConnection("Data Source=" & lcdb_dir & "u5-tests.lcdb;Version=3;")
                cn.Open()
                Using cmd As New SQLiteCommand("SELECT Count(*) FROM sqlite_master WHERE type='table'", cn)
                    For i = 1 To Convert.ToInt32(cmd.ExecuteScalar()) - 1
                        _test = New test
                        _test.Number = i
                        Using acmd As New SQLiteCommand("SELECT * FROM l" & i & "_test", cn)
                            Using rs = acmd.ExecuteReader
                                While rs.Read
                                    _question = New question
                                    Dim TestHtml = New System.Text.StringBuilder
                                    TestHtml.AppendLine("<head><style>" & TestCss & "</style></head>")
                                    TestHtml.AppendLine("<body><div id=""wrapper"">" & rs("given") & "</div></body>")

                                    _question.ID = Convert.ToInt32(rs("ID"))
                                    _question.Given = TestHtml.ToString
                                    _question.Type = rs("type")
                                    _question.Language = rs("lang_foc")
                                    _question.ClassName = rs("class_name")
                                    _question.Tries = Convert.ToInt32(rs("tries"))
                                    _question.Code = rs("code")
                                    Dim _choices() = rs("choices").ToString.Split("\")
                                    _question.Choices.AddRange(_choices)
                                    _question.Answer = rs("answer")
                                    _question.Misc.EssayLimit = Convert.ToInt32(rs("es_limit"))
                                    Dim _keywords() = rs("id_key").ToString.Split("\")
                                    _question.Misc.IdentificationKeywords.AddRange(_keywords)
                                    Dim _inputs() = rs("cd_inputs").ToString.Split("\")
                                    _question.Misc.CodingInputs.AddRange(_inputs)
                                    Dim _outputs() = rs("cd_outputs").ToString.Split("\")
                                    _question.Misc.CodingOutputs.AddRange(_outputs)
                                    _test.Questions.Add(_question)
                                End While
                            End Using
                        End Using
                        PHP.Lessons(i - 1).Test = _test
                    Next
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub
    Public Sub UpdateTestProgress()
        Using cn As New SQLiteConnection("Data Source=" & users_dir & "\" & current_user.Username & "\" & current_user.Username & ".db")
            cn.Open()
            Dim dbSql As New System.Text.StringBuilder
            If Equals(current_unit, Fundamentals) Then
                dbSql.AppendLine("INSERT INTO fundamentals_test VALUES (" & current_test.Number & ", " & score & ", " & percentage & ");")
            ElseIf Equals(current_unit, Java) Then
                dbSql.AppendLine("INSERT INTO java_test VALUES (" & current_test.Number & ", " & score & ", " & percentage & ");")
            ElseIf Equals(current_unit, CPP) Then
                dbSql.AppendLine("INSERT INTO cpp_test VALUES (" & current_test.Number & ", " & score & ", " & percentage & ");")
            ElseIf Equals(current_unit, HTML) Then
                dbSql.AppendLine("INSERT INTO html_test VALUES (" & current_test.Number & ", " & score & ", " & percentage & ");")
            ElseIf Equals(current_unit, JavaScript) Then
                dbSql.AppendLine("INSERT INTO javascript_test VALUES (" & current_test.Number & ", " & score & ", " & percentage & ");")
            ElseIf Equals(current_unit, PHP) Then
                dbSql.AppendLine("INSERT INTO php_test VALUES (" & current_test.Number & ", " & score & ", " & percentage & ");")
            End If
            Using cmd As New SQLiteCommand(dbSql.ToString, cn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Module
