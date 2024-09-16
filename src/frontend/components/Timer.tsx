'use client'
import { Skeleton } from "antd"
export default function Timer() {
    console.log("TIMER");
    return (
        <div>
            <Skeleton loading={true} />
        </div>
    )
}