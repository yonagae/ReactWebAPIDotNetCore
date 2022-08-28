import { useState, useEffect } from 'react';
import { PieChart, Pie, Cell, Tooltip, Legend } from "recharts";


export default function CustomLabelPieChart(prop) {
    const COLORS = ["#8884d8", "#82ca9d", "#FFBB28", "#FF8042", "#AF19FF"];
    let pieChartData = [];
    let totalSum = 0;

    const CustomTooltip = ({ active, payload, label }) => {
        if (active) {
            return (
                <div
                    className="custom-tooltip"
                    style={{
                        backgroundColor: "#ffff",
                        padding: "5px",
                        border: "1px solid #cccc"
                    }}
                >
                    <label>{`${payload[0].name} : $${payload[0].value} : ${((payload[0].value * 100) / totalSum).toFixed(2)}%`}</label>
                </div>
            );
        }
    }

    pieChartData = prop.fillPieData();
    
    pieChartData.forEach(element => {
        totalSum += element.value;
    })

    return (
        <>
            <PieChart width={730} height={300}>
                <Pie
                    data={pieChartData}
                    color="#000000"
                    dataKey="value"
                    nameKey="name"
                    cx="50%"
                    cy="50%"
                    outerRadius={120}
                    fill="#8884d8"
                >
                    {pieChartData.map((entry, index) => (
                        <Cell
                            key={`cell-${index}`}
                            fill={COLORS[index % COLORS.length]}
                        />
                    ))}
                </Pie>
                <Tooltip content={<CustomTooltip />} />
                <Legend />
            </PieChart>
        </>
    );
}