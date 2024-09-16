import { SessionContext } from "@/types/Session";
import { createContext, ReactNode, useContext } from "react";

const Context = createContext<SessionContext | null>(null);

export function SessionProvider({ children, value }: { children: ReactNode, value:SessionContext }) {
    return (
        <Context.Provider value={value}>
            {children}
        </Context.Provider>
    )
}

export function useAssessmentContext() {
    const context = useContext(Context);
    if (context === undefined) throw new Error("Session context used outside of the provider")
    return context
}