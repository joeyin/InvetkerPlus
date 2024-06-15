const chartOptions = {
    credits: {
        enabled: false,
    },
    tooltip: {
        enabled: false,
    },
    plotOptions: {
        line: {
            marker: {
                enabled: false
            }
        },
        area: {
            fillColor: "rgba(245, 197, 78, 0.31)",
            marker: {
                enabled: false,
            },
            lineWidth: 2,
            lineColor: "rgba(245, 197, 79, 0.21)",
            threshold: null,
            states: {
                hover: {
                    enabled: false,
                },
            },
        },
    },
    title: {
        text: "",
    },
    xAxis: {
        type: "datetime",
        labels: {
            enabled: false,
        },
        tickWidth: 0,
        lineWidth: 0,
        minPadding: 0,
        maxPadding: 0,
    },
    yAxis: {
        title: {
            text: "",
        },
        labels: {
            enabled: false,
        },
        gridLineWidth: 0,
    },
    legend: {
        enabled: false,
    },
};

const mockData = [
    {
        data: [
            [1262304000000, 0.7537],
            [1262563200000, 0.6951],
            [1262649600000, 0.6925],
            [1262736000000, 0.697],
            [1262822400000, 0.6992],
            [1262908800000, 0.7007],
            [1263168000000, 0.6884],
            [1263254400000, 0.6907],
            [1263340800000, 0.6868],
        ],
    },
];

$(window).ready(async () => {
    const apple = await fetch(
        "https://demo-live-data.highcharts.com/aapl-c.json"
    ).then((response) => response.json());

    Highcharts.chart({
        ...chartOptions,
        chart: {
            type: "area",
            renderTo: document.querySelector("#net-liquidity #chart"),
            backgroundColor: "transparent",
            margin: [98, 0, 0, 0],
        },
        series: mockData,
    });

    Highcharts.chart({
        ...chartOptions,
        chart: {
            type: "area",
            renderTo: document.querySelector("#portfolio-performance #chart"),
            backgroundColor: "transparent",
            margin: [98, 0, 0, 0],
        },
        rangeSelector: {
            x: -17,
            y: -32,
            labelStyle: {
                display: 'none'
            },
            buttonSpacing: 10,
            buttonTheme: {
                fill: 'none',
                stroke: 'rgba(245, 197, 78, 1)',
                'stroke-width': 1,
                r: 12,
                // paddingTop: 2,
                // paddingBottom: 2,
                paddingLeft: 7,
                paddingRight: 7,
                style: {
                    color: 'rgba(245, 197, 78, 1)',
                    fontSize: 12
                },
                states: {
                    hover: {
                        fill: "rgba(245, 197, 78, 1)",
                        style: {
                            color: "white",
                        },
                    },
                    select: {
                        fill: "rgba(245, 197, 78, 1)",
                        style: {
                            color: "white",
                        },
                    },
                },
            },
            buttons: [
                {
                    type: "week",
                    count: 1,
                    text: "1W",
                },
                {
                    type: "month",
                    count: 1,
                    text: "MTD",
                },
                {
                    type: "month",
                    count: 12,
                    text: "YTD",
                },
                {
                    type: "all",
                    count: 1,
                    text: "All",
                },
            ],
            selected: 2,
            inputEnabled: false,
            enabled: true,
        },
        tooltip: {
            enabled: true,
        },
        series: [
            {
                name: "AAPL",
                data: apple,
                marker: {
                    enabled: false,
                },
                tooltip: {
                    valueDecimals: 2,
                },
            },
        ],
    });
});
