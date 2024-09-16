import { Dispatch, SetStateAction } from "react";

export interface Session {
    grammar: string,
    listening: string,
    speaking: string,
}

export interface SessionContext {
    setSession: Dispatch<SetStateAction<Session>>,
    session: Session
}