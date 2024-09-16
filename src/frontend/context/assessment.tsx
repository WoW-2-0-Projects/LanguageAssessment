'use client'
import { createContext, useContext, ReactNode } from "react";
import { AssessmentContext } from "@/types/Assessment";

const Context = createContext<AssessmentContext | null>(null)

export function AssessmentProvider({ children, value }: { children: ReactNode, value:AssessmentContext }) {
    return (
        <Context.Provider value={value}>
            {children}
        </Context.Provider>
    )
}

export function useAssessmentContext() {
    const context = useContext(Context);
    if (context === undefined) throw new Error("Assessment context used outside of the provider")
    return context
}