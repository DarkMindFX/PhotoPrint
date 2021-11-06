SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.p_Country_Populate'))
   DROP PROC dbo.p_Country_Populate
GO

CREATE PROCEDURE dbo.p_Country_Populate 
	
AS
BEGIN

	DECLARE @tblCountry AS TABLE (
		[ID] [bigint] NOT NULL,
		[ISO] [nvarchar](5) NOT NULL,
		[CountryName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblCountry
	SELECT 1, 'AF', 'Afghanistan' UNION
	SELECT 2, 'AL', 'Albania' UNION
	SELECT 3, 'DZ', 'Algeria' UNION
	SELECT 4, 'AS', 'American Samoa' UNION
	SELECT 5, 'AD', 'Andorra' UNION
	SELECT 6, 'AO', 'Angola' UNION
	SELECT 7, 'AI', 'Anguilla' UNION
	SELECT 8, 'AQ', 'Antarctica' UNION
	SELECT 9, 'AG', 'Antigua and Barbuda' UNION
	SELECT 10, 'AR', 'Argentina' UNION
	SELECT 11, 'AM', 'Armenia' UNION
	SELECT 12, 'AW', 'Aruba' UNION
	SELECT 13, 'AU', 'Australia' UNION
	SELECT 14, 'AT', 'Austria' UNION
	SELECT 15, 'AZ', 'Azerbaijan' UNION
	SELECT 16, 'BS', 'Bahamas' UNION
	SELECT 17, 'BH', 'Bahrain' UNION
	SELECT 18, 'BD', 'Bangladesh' UNION
	SELECT 19, 'BB', 'Barbados' UNION
	SELECT 20, 'BY', 'Belarus' UNION
	SELECT 21, 'BE', 'Belgium' UNION
	SELECT 22, 'BZ', 'Belize' UNION
	SELECT 23, 'BJ', 'Benin' UNION
	SELECT 24, 'BM', 'Bermuda' UNION
	SELECT 25, 'BT', 'Bhutan' UNION
	SELECT 26, 'BO', 'Bolivia (Plurinational State of)' UNION
	SELECT 27, 'BQ', 'Bonaire, Sint Eustatius and Saba' UNION
	SELECT 28, 'BA', 'Bosnia and Herzegovina' UNION
	SELECT 29, 'BW', 'Botswana' UNION
	SELECT 30, 'BV', 'Bouvet Island' UNION
	SELECT 31, 'BR', 'Brazil' UNION
	SELECT 32, 'IO', 'British Indian Ocean Territory' UNION
	SELECT 33, 'BN', 'Brunei Darussalam' UNION
	SELECT 34, 'BG', 'Bulgaria' UNION
	SELECT 35, 'BF', 'Burkina Faso' UNION
	SELECT 36, 'BI', 'Burundi' UNION
	SELECT 37, 'CV', 'Cabo Verde' UNION
	SELECT 38, 'KH', 'Cambodia' UNION
	SELECT 39, 'CM', 'Cameroon' UNION
	SELECT 40, 'CA', 'Canada' UNION
	SELECT 41, 'KY', 'Cayman Islands' UNION
	SELECT 42, 'CF', 'Central African Republic' UNION
	SELECT 43, 'TD', 'Chad' UNION
	SELECT 44, 'CL', 'Chile' UNION
	SELECT 45, 'CN', 'China' UNION
	SELECT 46, 'CX', 'Christmas Island' UNION
	SELECT 47, 'CC', 'Cocos (Keeling) Islands' UNION
	SELECT 48, 'CO', 'Colombia' UNION
	SELECT 49, 'KM', 'Comoros' UNION
	SELECT 50, 'CD', 'Congo (the Democratic Republic of the)' UNION
	SELECT 51, 'CG', 'Congo' UNION
	SELECT 52, 'CK', 'Cook Islands' UNION
	SELECT 53, 'CR', 'Costa Rica' UNION
	SELECT 54, 'HR', 'Croatia' UNION
	SELECT 55, 'CU', 'Cuba' UNION
	SELECT 56, 'CW', 'Curaçao' UNION
	SELECT 57, 'CY', 'Cyprus' UNION
	SELECT 58, 'CZ', 'Czechia' UNION
	SELECT 59, 'CI', 'Côte dIvoire' UNION
	SELECT 60, 'DK', 'Denmark' UNION
	SELECT 61, 'DJ', 'Djibouti' UNION
	SELECT 62, 'DM', 'Dominica' UNION
	SELECT 63, 'DO', 'Dominican Republic' UNION
	SELECT 64, 'EC', 'Ecuador' UNION
	SELECT 65, 'EG', 'Egypt' UNION
	SELECT 66, 'SV', 'El Salvador' UNION
	SELECT 67, 'GQ', 'Equatorial Guinea' UNION
	SELECT 68, 'ER', 'Eritrea' UNION
	SELECT 69, 'EE', 'Estonia' UNION
	SELECT 70, 'SZ', 'Eswatini' UNION
	SELECT 71, 'ET', 'Ethiopia' UNION
	SELECT 72, 'FK', 'Falkland Islands [Malvinas]' UNION
	SELECT 73, 'FO', 'Faroe Islands' UNION
	SELECT 74, 'FJ', 'Fiji' UNION
	SELECT 75, 'FI', 'Finland' UNION
	SELECT 76, 'FR', 'France' UNION
	SELECT 77, 'GF', 'French Guiana' UNION
	SELECT 78, 'PF', 'French Polynesia' UNION
	SELECT 79, 'TF', 'French Southern Territories' UNION
	SELECT 80, 'GA', 'Gabon' UNION
	SELECT 81, 'GM', 'Gambia' UNION
	SELECT 82, 'GE', 'Georgia' UNION
	SELECT 83, 'DE', 'Germany' UNION
	SELECT 84, 'GH', 'Ghana' UNION
	SELECT 85, 'GI', 'Gibraltar' UNION
	SELECT 86, 'GR', 'Greece' UNION
	SELECT 87, 'GL', 'Greenland' UNION
	SELECT 88, 'GD', 'Grenada' UNION
	SELECT 89, 'GP', 'Guadeloupe' UNION
	SELECT 90, 'GU', 'Guam' UNION
	SELECT 91, 'GT', 'Guatemala' UNION
	SELECT 92, 'GG', 'Guernsey' UNION
	SELECT 93, 'GN', 'Guinea' UNION
	SELECT 94, 'GW', 'Guinea-Bissau' UNION
	SELECT 95, 'GY', 'Guyana' UNION
	SELECT 96, 'HT', 'Haiti' UNION
	SELECT 97, 'HM', 'Heard Island and McDonald Islands' UNION
	SELECT 98, 'VA', 'Holy See' UNION
	SELECT 99, 'HN', 'Honduras' UNION
	SELECT 100, 'HK', 'Hong Kong' UNION
	SELECT 101, 'HU', 'Hungary' UNION
	SELECT 102, 'IS', 'Iceland' UNION
	SELECT 103, 'IN', 'India' UNION
	SELECT 104, 'ID', 'Indonesia' UNION
	SELECT 105, 'IR', 'Iran (Islamic Republic of)' UNION
	SELECT 106, 'IQ', 'Iraq' UNION
	SELECT 107, 'IE', 'Ireland' UNION
	SELECT 108, 'IM', 'Isle of Man' UNION
	SELECT 109, 'IL', 'Israel' UNION
	SELECT 110, 'IT', 'Italy' UNION
	SELECT 111, 'JM', 'Jamaica' UNION
	SELECT 112, 'JP', 'Japan' UNION
	SELECT 113, 'JE', 'Jersey' UNION
	SELECT 114, 'JO', 'Jordan' UNION
	SELECT 115, 'KZ', 'Kazakhstan' UNION
	SELECT 116, 'KE', 'Kenya' UNION
	SELECT 117, 'KI', 'Kiribati' UNION
	SELECT 118, 'KP', 'Korea (the Democratic People''s Republic of)' UNION
	SELECT 119, 'KR', 'Korea (the Republic of)' UNION
	SELECT 120, 'KW', 'Kuwait' UNION
	SELECT 121, 'KG', 'Kyrgyzstan' UNION
	SELECT 122, 'LA', 'Lao People''s Democratic Republic' UNION
	SELECT 123, 'LV', 'Latvia' UNION
	SELECT 124, 'LB', 'Lebanon' UNION
	SELECT 125, 'LS', 'Lesotho' UNION
	SELECT 126, 'LR', 'Liberia' UNION
	SELECT 127, 'LY', 'Libya' UNION
	SELECT 128, 'LI', 'Liechtenstein' UNION
	SELECT 129, 'LT', 'Lithuania' UNION
	SELECT 130, 'LU', 'Luxembourg' UNION
	SELECT 131, 'MO', 'Macao' UNION
	SELECT 132, 'MG', 'Madagascar' UNION
	SELECT 133, 'MW', 'Malawi' UNION
	SELECT 134, 'MY', 'Malaysia' UNION
	SELECT 135, 'MV', 'Maldives' UNION
	SELECT 136, 'ML', 'Mali' UNION
	SELECT 137, 'MT', 'Malta' UNION
	SELECT 138, 'MH', 'Marshall Islands' UNION
	SELECT 139, 'MQ', 'Martinique' UNION
	SELECT 140, 'MR', 'Mauritania' UNION
	SELECT 141, 'MU', 'Mauritius' UNION
	SELECT 142, 'YT', 'Mayotte' UNION
	SELECT 143, 'MX', 'Mexico' UNION
	SELECT 144, 'FM', 'Micronesia (Federated States of)' UNION
	SELECT 145, 'MD', 'Moldova (the Republic of)' UNION
	SELECT 146, 'MC', 'Monaco' UNION
	SELECT 147, 'MN', 'Mongolia' UNION
	SELECT 148, 'ME', 'Montenegro' UNION
	SELECT 149, 'MS', 'Montserrat' UNION
	SELECT 150, 'MA', 'Morocco' UNION
	SELECT 151, 'MZ', 'Mozambique' UNION
	SELECT 152, 'MM', 'Myanmar' UNION
	SELECT 153, 'NA', 'Namibia' UNION
	SELECT 154, 'NR', 'Nauru' UNION
	SELECT 155, 'NP', 'Nepal' UNION
	SELECT 156, 'NL', 'Netherlands' UNION
	SELECT 157, 'NC', 'New Caledonia' UNION
	SELECT 158, 'NZ', 'New Zealand' UNION
	SELECT 159, 'NI', 'Nicaragua' UNION
	SELECT 160, 'NE', 'Niger' UNION
	SELECT 161, 'NG', 'Nigeria' UNION
	SELECT 162, 'NU', 'Niue' UNION
	SELECT 163, 'NF', 'Norfolk Island' UNION
	SELECT 164, 'MP', 'Northern Mariana Islands' UNION
	SELECT 165, 'NO', 'Norway' UNION
	SELECT 166, 'OM', 'Oman' UNION
	SELECT 167, 'PK', 'Pakistan' UNION
	SELECT 168, 'PW', 'Palau' UNION
	SELECT 169, 'PS', 'Palestine, State of' UNION
	SELECT 170, 'PA', 'Panama' UNION
	SELECT 171, 'PG', 'Papua New Guinea' UNION
	SELECT 172, 'PY', 'Paraguay' UNION
	SELECT 173, 'PE', 'Peru' UNION
	SELECT 174, 'PH', 'Philippines' UNION
	SELECT 175, 'PN', 'Pitcairn' UNION
	SELECT 176, 'PL', 'Poland' UNION
	SELECT 177, 'PT', 'Portugal' UNION
	SELECT 178, 'PR', 'Puerto Rico' UNION
	SELECT 179, 'QA', 'Qatar' UNION
	SELECT 180, 'MK', 'Republic of North Macedonia' UNION
	SELECT 181, 'RO', 'Romania' UNION
	SELECT 182, 'RU', 'Russian Federation' UNION
	SELECT 183, 'RW', 'Rwanda' UNION
	SELECT 184, 'RE', 'Réunion' UNION
	SELECT 185, 'BL', 'Saint Barthelemy' UNION
	SELECT 186, 'SH', 'Saint Helena, Ascension and Tristan da Cunha' UNION
	SELECT 187, 'KN', 'Saint Kitts and Nevis' UNION
	SELECT 188, 'LC', 'Saint Lucia' UNION
	SELECT 189, 'MF', 'Saint Martin (French part)' UNION
	SELECT 190, 'PM', 'Saint Pierre and Miquelon' UNION
	SELECT 191, 'VC', 'Saint Vincent and the Grenadines' UNION
	SELECT 192, 'WS', 'Samoa' UNION
	SELECT 193, 'SM', 'San Marino' UNION
	SELECT 194, 'ST', 'Sao Tome and Principe' UNION
	SELECT 195, 'SA', 'Saudi Arabia' UNION
	SELECT 196, 'SN', 'Senegal' UNION
	SELECT 197, 'RS', 'Serbia' UNION
	SELECT 198, 'SC', 'Seychelles' UNION
	SELECT 199, 'SL', 'Sierra Leone' UNION
	SELECT 200, 'SG', 'Singapore' UNION
	SELECT 201, 'SX', 'Sint Maarten (Dutch part)' UNION
	SELECT 202, 'SK', 'Slovakia' UNION
	SELECT 203, 'SI', 'Slovenia' UNION
	SELECT 204, 'SB', 'Solomon Islands' UNION
	SELECT 205, 'SO', 'Somalia' UNION
	SELECT 206, 'ZA', 'South Africa' UNION
	SELECT 207, 'GS', 'South Georgia and the South Sandwich Islands' UNION
	SELECT 208, 'SS', 'South Sudan' UNION
	SELECT 209, 'ES', 'Spain' UNION
	SELECT 210, 'LK', 'Sri Lanka' UNION
	SELECT 211, 'SD', 'Sudan' UNION
	SELECT 212, 'SR', 'Suriname' UNION
	SELECT 213, 'SJ', 'Svalbard and Jan Mayen' UNION
	SELECT 214, 'SE', 'Sweden' UNION
	SELECT 215, 'CH', 'Switzerland' UNION
	SELECT 216, 'SY', 'Syrian Arab Republic' UNION
	SELECT 217, 'TW', 'Taiwan (Province of China)' UNION
	SELECT 218, 'TJ', 'Tajikistan' UNION
	SELECT 219, 'TZ', 'Tanzania, United Republic of' UNION
	SELECT 220, 'TH', 'Thailand' UNION
	SELECT 221, 'TL', 'Timor-Leste' UNION
	SELECT 222, 'TG', 'Togo' UNION
	SELECT 223, 'TK', 'Tokelau' UNION
	SELECT 224, 'TO', 'Tonga' UNION
	SELECT 225, 'TT', 'Trinidad and Tobago' UNION
	SELECT 226, 'TN', 'Tunisia' UNION
	SELECT 227, 'TR', 'Turkey' UNION
	SELECT 228, 'TM', 'Turkmenistan' UNION
	SELECT 229, 'TC', 'Turks and Caicos Islands' UNION
	SELECT 230, 'TV', 'Tuvalu' UNION
	SELECT 231, 'UG', 'Uganda' UNION
	SELECT 232, 'UA', 'Ukraine' UNION
	SELECT 233, 'AE', 'United Arab Emirates' UNION
	SELECT 234, 'GB', 'United Kingdom of Great Britain ? Northern Ireland' UNION
	SELECT 235, 'UM', 'United States Minor Outlying Islands' UNION
	SELECT 236, 'US', 'United States of America' UNION
	SELECT 237, 'UY', 'Uruguay' UNION
	SELECT 238, 'UZ', 'Uzbekistan' UNION
	SELECT 239, 'VU', 'Vanuatu' UNION
	SELECT 240, 'VE', 'Venezuela (Bolivarian Republic of)' UNION
	SELECT 241, 'VN', 'Viet Nam' UNION
	SELECT 242, 'VG', 'Virgin Islands (British)' UNION
	SELECT 243, 'VI', 'Virgin Islands (U.S.)' UNION
	SELECT 244, 'WF', 'Wallis and Futuna' UNION
	SELECT 245, 'EH', 'Western Sahara' UNION
	SELECT 246, 'YE', 'Yemen' UNION
	SELECT 247, 'ZM', 'Zambia' UNION
	SELECT 248, 'ZW', 'Zimbabwe' 
	


	SET IDENTITY_INSERT dbo.Country ON;

	MERGE dbo.Country AS t
	USING @tblCountry AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[CountryName] = s.[CountryName],
			t.[ISO] = s.[ISO]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[CountryName], [ISO]) 
		VALUES (s.[ID], s.[CountryName], s.[ISO])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.Country OFF;
	
	SET NOCOUNT ON;
END
GO
