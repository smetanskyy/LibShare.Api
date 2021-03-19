namespace LibShare.Api.Data.Constants
{
    public static class HtmlStrings
    {
        public static string GetHtmlEmailForRestorePassword(string url)
        {
            string html = $@"
        <!-- start body -->
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">

    <!-- start logo -->
    <tr>
      <td align=""center"" bgcolor=""#e9ecef"">
        <!--[if (gte mso 9)|(IE)]>
        <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
        <tr>
        <td align=""center"" valign=""top"" width=""600"">
        <![endif]-->
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
          <tr>
            <td align=""center"" valign=""top"" style=""padding: 36px 24px;"">
              <a href=""{url}"" target=""_blank"" style=""display: inline-block;"">
                <img src=""{Base64Images.Logo}"" alt=""Logo"" border=""0"" width=""48"" style=""display: block; width: 48px; max-width: 48px; min-width: 48px;"">
              </a>
            </td>
          </tr>
        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end logo -->

    <!-- start hero -->
    <tr>
      <td align=""center"" bgcolor=""#e9ecef"">
        <!--[if (gte mso 9)|(IE)]>
        <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
        <tr>
        <td align=""center"" valign=""top"" width=""600"">
        <![endif]-->
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
          <tr>
            <td align=""left"" bgcolor=""#ffffff"" style=""padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;"">
              <h1 style=""margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;"">Відновлення пароля</h1>
            </td>
          </tr>
        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end hero -->

    <!-- start copy block -->
    <tr>
      <td align=""center"" bgcolor=""#e9ecef"">
        <!--[if (gte mso 9)|(IE)]>
        <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
        <tr>
        <td align=""center"" valign=""top"" width=""600"">
        <![endif]-->
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">

          <!-- start copy -->
          <tr>
            <td align=""left"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;"">
              <p style=""margin: 0;"">Натисніть кнопку нижче, для відновлення пароля облікового запису клієнта.</p>
            </td>
          </tr>
          <!-- end copy -->

          <!-- start button -->
          <tr>
            <td align=""left"" bgcolor=""#ffffff"">
              <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                <tr>
                  <td align=""center"" bgcolor=""#ffffff"" style=""padding: 12px;"">
                    <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                      <tr>
                        <td align=""center"" bgcolor=""#1a82e2"" style=""border-radius: 6px;"">
                          <a href=""{url}"" target=""_blank"" style=""display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;"">Відновити пароль</a>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <!-- end button -->

          <!-- start copy -->
          <tr>
            <td align=""left"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;"">
              <p style=""margin: 0;"">Якщо це не працює, скопіюйте і вставте наступне посилання в браузер:</p>
              <p style=""margin: 0;""><a href=""{url}"" target=""_blank"">{url}</a></p>
            </td>
          </tr>
          <!-- end copy -->

          <!-- start copy -->
          <tr>
            <td align=""left"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf"">
              <p style=""margin: 0;"">З повагою, команда підтримки!</p>
            </td>
          </tr>
          <!-- end copy -->

        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end copy block -->

    <!-- start footer -->
    <tr>
      <td align=""center"" bgcolor=""#e9ecef"" style=""padding: 24px;"">
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">

          <!-- start permission -->
          <tr>
            <td align=""center"" bgcolor=""#e9ecef"" style=""padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;"">
              <p style=""margin: 0;"">Ви отримали даний лист, тому що Ви відправили запит на зміну пароля. Якщо Ви цього не робили видаліть даний лист!</p>
            </td>
          </tr>
          <!-- end permission -->

          <!-- start unsubscribe -->
          <tr>
            <td align=""center"" bgcolor=""#e9ecef"" style=""padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;"">
              <p style=""margin: 0;"">Щоб не отримувати листи з нашого ресурсу, ви можете <a href=""{url}"" target=""_blank"">відписатися</a> в будь-який час.</p>
              <p style=""margin: 0;""></p>
            </td>
          </tr>
          <!-- end unsubscribe -->

        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end footer -->";

            return html;
        }

        public static string GetHtmlEmailForConfirmAccount(string url)
        {
            string html = $@"
        <!-- start body -->
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">

    <!-- start logo -->
    <tr>
      <td align=""center"" bgcolor=""#e9ecef"">
        <!--[if (gte mso 9)|(IE)]>
        <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
        <tr>
        <td align=""center"" valign=""top"" width=""600"">
        <![endif]-->
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
          <tr>
            <td align=""center"" valign=""top"" style=""padding: 36px 24px;"">
              <a href=""{url}"" target=""_blank"" style=""display: inline-block;"">
                <img src=""{Base64Images.Logo}"" alt=""Logo"" border=""0"" width=""48"" style=""display: block; width: 48px; max-width: 48px; min-width: 48px;"">
              </a>
            </td>
          </tr>
        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end logo -->

    <!-- start hero -->
    <tr>
      <td align=""center"" bgcolor=""#e9ecef"">
        <!--[if (gte mso 9)|(IE)]>
        <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
        <tr>
        <td align=""center"" valign=""top"" width=""600"">
        <![endif]-->
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
          <tr>
            <td align=""left"" bgcolor=""#ffffff"" style=""padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;"">
              <h1 style=""margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;"">Підтвердження електронної пошти</h1>
            </td>
          </tr>
        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end hero -->

    <!-- start copy block -->
    <tr>
      <td align=""center"" bgcolor=""#e9ecef"">
        <!--[if (gte mso 9)|(IE)]>
        <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
        <tr>
        <td align=""center"" valign=""top"" width=""600"">
        <![endif]-->
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">

          <!-- start copy -->
          <tr>
            <td align=""left"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;"">
              <p style=""margin: 0;"">Натисніть кнопку нижче, для підтвердження електронної пошти.</p>
            </td>
          </tr>
          <!-- end copy -->

          <!-- start button -->
          <tr>
            <td align=""left"" bgcolor=""#ffffff"">
              <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                <tr>
                  <td align=""center"" bgcolor=""#ffffff"" style=""padding: 12px;"">
                    <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                      <tr>
                        <td align=""center"" bgcolor=""#1a82e2"" style=""border-radius: 6px;"">
                          <a href=""{url}"" target=""_blank"" style=""display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;"">Підтвердити електронну пошту</a>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <!-- end button -->

          <!-- start copy -->
          <tr>
            <td align=""left"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;"">
              <p style=""margin: 0;"">Якщо це не працює, скопіюйте і вставте наступне посилання в браузер:</p>
              <p style=""margin: 0;""><a href=""{url}"" target=""_blank"">{url}</a></p>
            </td>
          </tr>
          <!-- end copy -->

          <!-- start copy -->
          <tr>
            <td align=""left"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf"">
              <p style=""margin: 0;"">З повагою, команда підтримки!</p>
            </td>
          </tr>
          <!-- end copy -->

        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end copy block -->

    <!-- start footer -->
    <tr>
      <td align=""center"" bgcolor=""#e9ecef"" style=""padding: 24px;"">
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">

          <!-- start permission -->
          <tr>
            <td align=""center"" bgcolor=""#e9ecef"" style=""padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;"">
              <p style=""margin: 0;"">Ви отримали даний лист, тому що Ви відправили запит на відновлення облікового запису. Якщо Ви цього не робили видаліть даний лист!</p>
            </td>
          </tr>
          <!-- end permission -->

          <!-- start unsubscribe -->
          <tr>
            <td align=""center"" bgcolor=""#e9ecef"" style=""padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;"">
              <p style=""margin: 0;"">Щоб не отримувати листи з нашого ресурсу, ви можете <a href=""{url}"" target=""_blank"">відписатися</a> в будь-який час.</p>
              <p style=""margin: 0;""></p>
            </td>
          </tr>
          <!-- end unsubscribe -->

        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end footer -->";

            return html;
        }
    }
}
