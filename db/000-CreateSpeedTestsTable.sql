CREATE TABLE [dbo].[SpeedTests](
	[SpeedTestId] [uniqueidentifier] NOT NULL,
	[SpeedTestTimestamp] [datetime2] NOT NULL,
	[Jitter] [float] NULL,
	[Latency] [float] NULL,
	[DownloadBandwidth] [int] NULL,
    [DownloadBytes] [int] NULL,
    [DownloadElapsed] [int] NULL,
    [UploadBandwidth] [int] NULL,
    [UploadBytes] [int] NULL,
    [UploadElapsed] [int] NULL,
    [PacketLoss] [float] NULL,
    [Isp] [varchar](200) NULL,
    [InternalIp] [varchar](50) NULL,
    [InterfaceName] [varchar](200) NULL,
    [MacAddress] [varchar](200) NULL,
    [IsVpn] [bit] NOT NULL,
    [ExternalIp] [varchar](50) NULL,
    [ServerId] [int] NULL,
    [ServerName] [varchar](200) NULL,
    [ServerLocation] [varchar](200) NULL,
    [ServerCountry] [varchar](200) NULL,
    [ServerHost] [varchar](200) NULL,
    [ServerPort] [int] NULL,
    [ServerIp] [varchar](50) NULL,
    [ResultId] [uniqueidentifier] NULL,
    [ResultUrl] [nvarchar](2083) NULL,
    PRIMARY KEY CLUSTERED 
    (
	    [SpeedTestId] ASC
    )
)
GO