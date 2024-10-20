'use client'

import { Button } from "flowbite-react";
import { signOut } from "next-auth/react";
import React from "react";

export default function LogoutButton() {
  return (
    <Button 
        outline 
        onClick={() => signOut()}
    >
      Logout
    </Button>
  );
}
