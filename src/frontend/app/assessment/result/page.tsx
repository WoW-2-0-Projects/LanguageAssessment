'use client'
import { Progress } from "antd";
import { useSearchParams } from "next/navigation";
import "./style.css"

export default function ResultPage() {
    const searchParams = useSearchParams();
    let id = searchParams.get("id");

    if (!id) {
        return <div>Result not found</div>
    }
    // const results = fetch("");
    const results = [
        {
            percent: 20,
            title: "grammar",
        },
        {
            percent: 58,
            title: "listening",
        },

    ]

    const progressMarkup = results.map(r => {
        let strokeColor;

        if (r.title.toLowerCase() === "grammar") {
            strokeColor = "#5AFB7A"
        }
        if (r.title.toLowerCase() === "listening") {
            strokeColor = "#3155FF"
        }

        return <div style={{ display: "flex", flexDirection: "column", alignItems: "center", color: "#fff" }}>
            <Progress type="circle" percent={r.percent} trailColor="#fff" strokeColor={strokeColor} />
            <h2>{r.title}</h2>
        </div>
    })

    return (
        <div>
            <h1 style={{ fontSize: "30px", marginBottom: "20px" }}>Test result for test #{id}</h1>
            <div style={{ display: "flex", gap: '30px' }}>
                {progressMarkup}
            </div>
        </div>

    )
}