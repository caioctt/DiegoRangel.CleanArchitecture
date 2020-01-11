using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Email;
using Microsoft.Extensions.Configuration;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Services.Email
{
	public class EmailTemplateProvider : IEmailTemplateProvider
	{
		private readonly IConfiguration _configuration;

		public EmailTemplateProvider(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string Build(string title, string body)
		{
			return $@"
                <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                <html xmlns='http://www.w3.org/1999/xhtml'>
	                <head>
		                <title>blazer</title>
		                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
		                <meta name='viewport' content='width=device-width, initial-scale=1.0' />
	                </head>
	                <body style='margin:0; padding:40px;' bgcolor='#eaeced'>
		                <table style='min-width:320px;' width='100%' cellspacing='0' cellpadding='0' bgcolor='#eaeced'>
			                <tr>
				                <td class='wrapper' style='padding:0 10px;'>
					                <table width='100%' cellpadding='0' cellspacing='0'>
						                <tr>
							                <td data-bgcolor='bg-module' bgcolor='#eaeced'>
								                <table class='flexible' width='600' align='center' style='margin:0 auto;' cellpadding='0' cellspacing='0'>
									                <tr>
										                <td data-bgcolor='bg-block' class='holder' style='padding:58px 60px 52px;' bgcolor='#ffffff'>
											                <table width='100%' cellpadding='0' cellspacing='0'>
												                <tr>
													                <td data-color='title' data-size='size title' data-min='25' data-max='45' data-link-color='link title color' data-link-style='text-decoration:none; color:#292c34;' class='title' align='center' style='font:35px/38px Arial, Helvetica, sans-serif; color:#292c34; padding:0 0 24px;'>
														                blazer
													                </td>
												                </tr>
												                <tr>
													                <td data-color='text' data-size='size text' data-min='10' data-max='26' data-link-color='link text color' align='center' style='color:#212529; padding:0 0 23px;'>
														                <h4 style='font-weight: 600'>{title}</h4>

                                                                        {body}
													                </td>
												                </tr>
											                </table>
										                </td>
									                </tr>
								                </table>
							                </td>
						                </tr>
					                </table>
						                <tr>
							                <td data-bgcolor='bg-module' bgcolor='#eaeced'>
								                <table class='flexible' width='600' align='center' style='margin:0 auto;' cellpadding='0' cellspacing='0'>
									                <tr>
										                <td class='footer' style='padding:20px 0 0 10px;'>
											                <table width='100%' cellpadding='0' cellspacing='0'>
												                <tr class='table-holder'>
													                <th class='tfoot' width='400' align='left' style='vertical-align:top; padding:0;'>
														                <table width='100%' cellpadding='0' cellspacing='0'>
															                <tr>
																                <td data-color='text' data-link-color='link text color' data-link-style='text-decoration:underline; color:#797c82;' class='aligncenter' style='font:12px/16px Arial, Helvetica, sans-serif; color:#797c82; padding:0 0 10px; text-align: center'>
																	                <b>blazer</b> - Todos os direitos reservados.
																                </td>
															                </tr>
														                </table>
													                </th>
												                </tr>
											                </table>
										                </td>
									                </tr>
								                </table>
							                </td>
						                </tr>
					                </table>
				                </td>
			                </tr>
		                </table>
	                </body>
                </html>
            ";
		}
	}
}