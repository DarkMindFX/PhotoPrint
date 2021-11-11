
CREATE PROCEDURE dbo.p_Currency_Populate 
	
AS
BEGIN

	DECLARE @tblCurrency AS TABLE (
		[ID] [bigint] NOT NULL,
		[ISO] [nvarchar](5) NOT NULL,
		[CurrencyName] [nvarchar](50) NOT NULL
	)

	INSERT INTO @tblCurrency
	SELECT 1, 'AFN', 'Afghani' UNION 
	SELECT 2, 'EUR', 'Euro' UNION 
	SELECT 3, 'ALL', 'Lek' UNION 
	SELECT 4, 'DZD', 'Algerian Dinar' UNION 
	SELECT 5, 'USD', 'US Dollar' UNION 
	SELECT 6, 'EUR', 'Euro' UNION 
	SELECT 7, 'AOA', 'Kwanza' UNION 
	SELECT 8, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 9, 'ARS', 'Argentine Peso' UNION 
	SELECT 10, 'AMD', 'Armenian Dram' UNION 
	SELECT 11, 'AWG', 'Aruban Florin' UNION 
	SELECT 12, 'AUD', 'Australian Dollar' UNION 
	SELECT 13, 'EUR', 'Euro' UNION 
	SELECT 14, 'AZN', 'Azerbaijanian Manat' UNION 
	SELECT 15, 'BSD', 'Bahamian Dollar' UNION 
	SELECT 16, 'BHD', 'Bahraini Dinar' UNION 
	SELECT 17, 'BDT', 'Taka' UNION 
	SELECT 18, 'BBD', 'Barbados Dollar' UNION 
	SELECT 19, 'BYR', 'Belarussian Ruble' UNION 
	SELECT 20, 'EUR', 'Euro' UNION 
	SELECT 21, 'BZD', 'Belize Dollar' UNION 
	SELECT 22, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 23, 'BMD', 'Bermudian Dollar' UNION 
	SELECT 24, 'BTN', 'Ngultrum' UNION 
	SELECT 25, 'INR', 'Indian Rupee' UNION 
	SELECT 26, 'BOB', 'Boliviano' UNION 
	SELECT 27, 'BOV', 'Mvdol' UNION 
	SELECT 28, 'USD', 'US Dollar' UNION 
	SELECT 29, 'BAM', 'Convertible Mark' UNION 
	SELECT 30, 'BWP', 'Pula' UNION 
	SELECT 31, 'NOK', 'Norwegian Krone' UNION 
	SELECT 32, 'BRL', 'Brazilian Real' UNION 
	SELECT 33, 'USD', 'US Dollar' UNION 
	SELECT 34, 'BND', 'Brunei Dollar' UNION 
	SELECT 35, 'BGN', 'Bulgarian Lev' UNION 
	SELECT 36, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 37, 'BIF', 'Burundi Franc' UNION 
	SELECT 38, 'KHR', 'Riel' UNION 
	SELECT 39, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 40, 'CAD', 'Canadian Dollar' UNION 
	SELECT 41, 'CVE', 'Cabo Verde Escudo' UNION 
	SELECT 42, 'KYD', 'Cayman Islands Dollar' UNION 
	SELECT 43, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 44, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 45, 'CLF', 'Unidad de Fomento' UNION 
	SELECT 46, 'CLP', 'Chilean Peso' UNION 
	SELECT 47, 'CNY', 'Yuan Renminbi' UNION 
	SELECT 48, 'AUD', 'Australian Dollar' UNION 
	SELECT 49, 'AUD', 'Australian Dollar' UNION 
	SELECT 50, 'COP', 'Colombian Peso' UNION 
	SELECT 51, 'COU', 'Unidad de Valor Real' UNION 
	SELECT 52, 'KMF', 'Comoro Franc' UNION 
	SELECT 53, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 54, 'CDF', 'Congolese Franc' UNION 
	SELECT 55, 'NZD', 'New Zealand Dollar' UNION 
	SELECT 56, 'CRC', 'Costa Rican Colon' UNION 
	SELECT 57, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 58, 'HRK', 'Croatian Kuna' UNION 
	SELECT 59, 'CUC', 'Peso Convertible' UNION 
	SELECT 60, 'CUP', 'Cuban Peso' UNION 
	SELECT 61, 'ANG', 'Netherlands Antillean Guilder' UNION 
	SELECT 62, 'EUR', 'Euro' UNION 
	SELECT 63, 'CZK', 'Czech Koruna' UNION 
	SELECT 64, 'DKK', 'Danish Krone' UNION 
	SELECT 65, 'DJF', 'Djibouti Franc' UNION 
	SELECT 66, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 67, 'DOP', 'Dominican Peso' UNION 
	SELECT 68, 'USD', 'US Dollar' UNION 
	SELECT 69, 'EGP', 'Egyptian Pound' UNION 
	SELECT 70, 'SVC', 'El Salvador Colon' UNION 
	SELECT 71, 'USD', 'US Dollar' UNION 
	SELECT 72, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 73, 'ERN', 'Nakfa' UNION 
	SELECT 74, 'EUR', 'Euro' UNION 
	SELECT 75, 'ETB', 'Ethiopian Birr' UNION 
	SELECT 76, 'EUR', 'Euro' UNION 
	SELECT 77, 'FKP', 'Falkland Islands Pound' UNION 
	SELECT 78, 'DKK', 'Danish Krone' UNION 
	SELECT 79, 'FJD', 'Fiji Dollar' UNION 
	SELECT 80, 'EUR', 'Euro' UNION 
	SELECT 81, 'EUR', 'Euro' UNION 
	SELECT 82, 'EUR', 'Euro' UNION 
	SELECT 83, 'XPF', 'CFP Franc' UNION 
	SELECT 84, 'EUR', 'Euro' UNION 
	SELECT 85, 'XAF', 'CFA Franc BEAC' UNION 
	SELECT 86, 'GMD', 'Dalasi' UNION 
	SELECT 87, 'GEL', 'Lari' UNION 
	SELECT 88, 'EUR', 'Euro' UNION 
	SELECT 89, 'GHS', 'Ghana Cedi' UNION 
	SELECT 90, 'GIP', 'Gibraltar Pound' UNION 
	SELECT 91, 'EUR', 'Euro' UNION 
	SELECT 92, 'DKK', 'Danish Krone' UNION 
	SELECT 93, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 94, 'EUR', 'Euro' UNION 
	SELECT 95, 'USD', 'US Dollar' UNION 
	SELECT 96, 'GTQ', 'Quetzal' UNION 
	SELECT 97, 'GBP', 'Pound Sterling' UNION 
	SELECT 98, 'GNF', 'Guinea Franc' UNION 
	SELECT 99, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 100, 'GYD', 'Guyana Dollar' UNION 
	SELECT 101, 'HTG', 'Gourde' UNION 
	SELECT 102, 'USD', 'US Dollar' UNION 
	SELECT 103, 'AUD', 'Australian Dollar' UNION 
	SELECT 104, 'EUR', 'Euro' UNION 
	SELECT 105, 'HNL', 'Lempira' UNION 
	SELECT 106, 'HKD', 'Hong Kong Dollar' UNION 
	SELECT 107, 'HUF', 'Forint' UNION 
	SELECT 108, 'ISK', 'Iceland Krona' UNION 
	SELECT 109, 'INR', 'Indian Rupee' UNION 
	SELECT 110, 'IDR', 'Rupiah' UNION 
	SELECT 111, 'XDR', 'SDR (Special Drawing Right)' UNION 
	SELECT 112, 'IRR', 'Iranian Rial' UNION 
	SELECT 113, 'IQD', 'Iraqi Dinar' UNION 
	SELECT 114, 'EUR', 'Euro' UNION 
	SELECT 115, 'GBP', 'Pound Sterling' UNION 
	SELECT 116, 'ILS', 'New Israeli Sheqel' UNION 
	SELECT 117, 'EUR', 'Euro' UNION 
	SELECT 118, 'JMD', 'Jamaican Dollar' UNION 
	SELECT 119, 'JPY', 'Yen' UNION 
	SELECT 120, 'GBP', 'Pound Sterling' UNION 
	SELECT 121, 'JOD', 'Jordanian Dinar' UNION 
	SELECT 122, 'KZT', 'Tenge' UNION 
	SELECT 123, 'KES', 'Kenyan Shilling' UNION 
	SELECT 124, 'AUD', 'Australian Dollar' UNION 
	SELECT 125, 'KPW', 'North Korean Won' UNION 
	SELECT 126, 'KRW', 'Won' UNION 
	SELECT 127, 'KWD', 'Kuwaiti Dinar' UNION 
	SELECT 128, 'KGS', 'Som' UNION 
	SELECT 129, 'LAK', 'Kip' UNION 
	SELECT 130, 'EUR', 'Euro' UNION 
	SELECT 131, 'LBP', 'Lebanese Pound' UNION 
	SELECT 132, 'LSL', 'Loti' UNION 
	SELECT 133, 'ZAR', 'Rand' UNION 
	SELECT 134, 'LRD', 'Liberian Dollar' UNION 
	SELECT 135, 'LYD', 'Libyan Dinar' UNION 
	SELECT 136, 'CHF', 'Swiss Franc' UNION 
	SELECT 137, 'EUR', 'Euro' UNION 
	SELECT 138, 'EUR', 'Euro' UNION 
	SELECT 139, 'MOP', 'Pataca' UNION 
	SELECT 140, 'MKD', 'Denar' UNION 
	SELECT 141, 'MGA', 'Malagasy Ariary' UNION 
	SELECT 142, 'MWK', 'Kwacha' UNION 
	SELECT 143, 'MYR', 'Malaysian Ringgit' UNION 
	SELECT 144, 'MVR', 'Rufiyaa' UNION 
	SELECT 145, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 146, 'EUR', 'Euro' UNION 
	SELECT 147, 'USD', 'US Dollar' UNION 
	SELECT 148, 'EUR', 'Euro' UNION 
	SELECT 149, 'MRO', 'Ouguiya' UNION 
	SELECT 150, 'MUR', 'Mauritius Rupee' UNION 
	SELECT 151, 'EUR', 'Euro' UNION 
	SELECT 152, 'XUA', 'ADB Unit of Account' UNION 
	SELECT 153, 'MXN', 'Mexican Peso' UNION 
	SELECT 154, 'MXV', 'Mexican Unidad de Inversion (UDI)' UNION 
	SELECT 155, 'USD', 'US Dollar' UNION 
	SELECT 156, 'MDL', 'Moldovan Leu' UNION 
	SELECT 157, 'EUR', 'Euro' UNION 
	SELECT 158, 'MNT', 'Tugrik' UNION 
	SELECT 159, 'EUR', 'Euro' UNION 
	SELECT 160, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 161, 'MAD', 'Moroccan Dirham' UNION 
	SELECT 162, 'MZN', 'Mozambique Metical' UNION 
	SELECT 163, 'MMK', 'Kyat' UNION 
	SELECT 164, 'NAD', 'Namibia Dollar' UNION 
	SELECT 165, 'ZAR', 'Rand' UNION 
	SELECT 166, 'AUD', 'Australian Dollar' UNION 
	SELECT 167, 'NPR', 'Nepalese Rupee' UNION 
	SELECT 168, 'EUR', 'Euro' UNION 
	SELECT 169, 'XPF', 'CFP Franc' UNION 
	SELECT 170, 'NZD', 'New Zealand Dollar' UNION 
	SELECT 171, 'NIO', 'Cordoba Oro' UNION 
	SELECT 172, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 173, 'NGN', 'Naira' UNION 
	SELECT 174, 'NZD', 'New Zealand Dollar' UNION 
	SELECT 175, 'AUD', 'Australian Dollar' UNION 
	SELECT 176, 'USD', 'US Dollar' UNION 
	SELECT 177, 'NOK', 'Norwegian Krone' UNION 
	SELECT 178, 'OMR', 'Rial Omani' UNION 
	SELECT 179, 'PKR', 'Pakistan Rupee' UNION 
	SELECT 180, 'USD', 'US Dollar' UNION 
	SELECT 181, 'PAB', 'Balboa' UNION 
	SELECT 182, 'USD', 'US Dollar' UNION 
	SELECT 183, 'PGK', 'Kina' UNION 
	SELECT 184, 'PYG', 'Guarani' UNION 
	SELECT 185, 'PEN', 'Nuevo Sol' UNION 
	SELECT 186, 'PHP', 'Philippine Peso' UNION 
	SELECT 187, 'NZD', 'New Zealand Dollar' UNION 
	SELECT 188, 'PLN', 'Zloty' UNION 
	SELECT 189, 'EUR', 'Euro' UNION 
	SELECT 190, 'USD', 'US Dollar' UNION 
	SELECT 191, 'QAR', 'Qatari Rial' UNION 
	SELECT 192, 'EUR', 'Euro' UNION 
	SELECT 193, 'RON', 'New Romanian Leu' UNION 
	SELECT 194, 'RUB', 'Russian Ruble' UNION 
	SELECT 195, 'RWF', 'Rwanda Franc' UNION 
	SELECT 196, 'EUR', 'Euro' UNION 
	SELECT 197, 'SHP', 'Saint Helena Pound' UNION 
	SELECT 198, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 199, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 200, 'EUR', 'Euro' UNION 
	SELECT 201, 'EUR', 'Euro' UNION 
	SELECT 202, 'XCD', 'East Caribbean Dollar' UNION 
	SELECT 203, 'WST', 'Tala' UNION 
	SELECT 204, 'EUR', 'Euro' UNION 
	SELECT 205, 'STD', 'Dobra' UNION 
	SELECT 206, 'SAR', 'Saudi Riyal' UNION 
	SELECT 207, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 208, 'RSD', 'Serbian Dinar' UNION 
	SELECT 209, 'SCR', 'Seychelles Rupee' UNION 
	SELECT 210, 'SLL', 'Leone' UNION 
	SELECT 211, 'SGD', 'Singapore Dollar' UNION 
	SELECT 212, 'ANG', 'Netherlands Antillean Guilder' UNION 
	SELECT 213, 'XSU', 'Sucre' UNION 
	SELECT 214, 'EUR', 'Euro' UNION 
	SELECT 215, 'EUR', 'Euro' UNION 
	SELECT 216, 'SBD', 'Solomon Islands Dollar' UNION 
	SELECT 217, 'SOS', 'Somali Shilling' UNION 
	SELECT 218, 'ZAR', 'Rand' UNION 
	SELECT 219, 'SSP', 'South Sudanese Pound' UNION 
	SELECT 220, 'EUR', 'Euro' UNION 
	SELECT 221, 'LKR', 'Sri Lanka Rupee' UNION 
	SELECT 222, 'SDG', 'Sudanese Pound' UNION 
	SELECT 223, 'SRD', 'Surinam Dollar' UNION 
	SELECT 224, 'NOK', 'Norwegian Krone' UNION 
	SELECT 225, 'SZL', 'Lilangeni' UNION 
	SELECT 226, 'SEK', 'Swedish Krona' UNION 
	SELECT 227, 'CHE', 'WIR Euro' UNION 
	SELECT 228, 'CHF', 'Swiss Franc' UNION 
	SELECT 229, 'CHW', 'WIR Franc' UNION 
	SELECT 230, 'SYP', 'Syrian Pound' UNION 
	SELECT 231, 'TWD', 'New Taiwan Dollar' UNION 
	SELECT 232, 'TJS', 'Somoni' UNION 
	SELECT 233, 'TZS', 'Tanzanian Shilling' UNION 
	SELECT 234, 'THB', 'Baht' UNION 
	SELECT 235, 'USD', 'US Dollar' UNION 
	SELECT 236, 'XOF', 'CFA Franc BCEAO' UNION 
	SELECT 237, 'NZD', 'New Zealand Dollar' UNION 
	SELECT 238, 'TOP', 'Paanga' UNION 
	SELECT 239, 'TTD', 'Trinidad and Tobago Dollar' UNION 
	SELECT 240, 'TND', 'Tunisian Dinar' UNION 
	SELECT 241, 'TRY', 'Turkish Lira' UNION 
	SELECT 242, 'TMT', 'Turkmenistan New Manat' UNION 
	SELECT 243, 'USD', 'US Dollar' UNION 
	SELECT 244, 'AUD', 'Australian Dollar' UNION 
	SELECT 245, 'UGX', 'Uganda Shilling' UNION 
	SELECT 246, 'UAH', 'Hryvnia' UNION 
	SELECT 247, 'AED', 'UAE Dirham' UNION 
	SELECT 248, 'GBP', 'Pound Sterling' UNION 
	SELECT 249, 'USD', 'US Dollar' UNION 
	SELECT 250, 'USN', 'US Dollar (Next day)' UNION 
	SELECT 251, 'USD', 'US Dollar' UNION 
	SELECT 252, 'UYI', 'Uruguay Peso en Unidades Indexadas (URUIURUI)' UNION 
	SELECT 253, 'UYU', 'Peso Uruguayo' UNION 
	SELECT 254, 'UZS', 'Uzbekistan Sum' UNION 
	SELECT 255, 'VUV', 'Vatu' UNION 
	SELECT 256, 'VEF', 'Bolivar' UNION 
	SELECT 257, 'VND', 'Dong' UNION 
	SELECT 258, 'USD', 'US Dollar' UNION 
	SELECT 259, 'USD', 'US Dollar' UNION 
	SELECT 260, 'XPF', 'CFP Franc' UNION 
	SELECT 261, 'MAD', 'Moroccan Dirham' UNION 
	SELECT 262, 'YER', 'Yemeni Rial' UNION 
	SELECT 263, 'ZMW', 'Zambian Kwacha' UNION 
	SELECT 264, 'ZWL', 'Zimbabwe Dollar'  

	


	SET IDENTITY_INSERT dbo.Currency ON;

	MERGE dbo.Currency AS t
	USING @tblCurrency AS s
	ON (t.ID = s.ID)
	WHEN MATCHED THEN
		UPDATE SET 
			t.[CurrencyName] = s.[CurrencyName],
			t.[ISO] = s.[ISO]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([ID],[CurrencyName], [ISO]) 
		VALUES (s.[ID], s.[CurrencyName], s.[ISO])
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE
	;
	SET IDENTITY_INSERT dbo.Currency OFF;
	
	SET NOCOUNT ON;
END
