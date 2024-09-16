'use client'

import { Endpoints } from "@/constants"
import { useEffect } from "react"

export default function ClientSession() {
    useEffect(() => {
        async function getSession() {
            try {
                const response = await fetch(Endpoints.startSession, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "ngrok-skip-browser-warning": "true"
                    },
                });
                const data = await response.json();
                console.log({data});
                if (data) {
                    sessionStorage.setItem('ll__session', JSON.stringify(data))
                }
            } catch (error) {
                console.error(error);
            }
        }
        getSession()
    }, [])
    return <></>
}