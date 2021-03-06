package main

import (
	"fmt"
	"path/filepath"
)

func main() {
	testAbs()
	testRel()
}

// Absolute path from relative path
func testAbs() {
	// https://golang.org/pkg/path/filepath/#Abs
	abs, err := filepath.Abs("../sys/FilepathAbsRel.ext")
	if err == nil {
		fmt.Println("Absolute:", abs)
	}
}

// Relative path to basepath
func testRel() {
	// https://golang.org/pkg/path/filepath/#Rel
	paths := []string{
		"/a/b/c",
		"/b/c",
		"./b/c",
	}

	base := "/a"
	fmt.Println("\nRelative to:", base)
	for _, p := range paths {
		rel, err := filepath.Rel(base, p)
		fmt.Printf("%q: %q %v\n", p, rel, err)
	}

	base = "/a/d/e.f"
	fmt.Println("\nRelative to:", base)
	for _, p := range paths {
		rel, err := filepath.Rel(base, p)
		fmt.Printf("%q: %q %v\n", p, rel, err)
	}

}

/*

$ go run FilepathAbsRel.go
Absolute: /home/.../lang/Go/src/sys/FilepathAbsRel.ext

Relative to: /a
"/a/b/c": "b/c" <nil>
"/b/c": "../b/c" <nil>
"./b/c": "" Rel: can't make ./b/c relative to /a

Relative to: /a/d/e.f
"/a/b/c": "../../b/c" <nil>
"/b/c": "../../../b/c" <nil>
"./b/c": "" Rel: can't make ./b/c relative to /a/d/e.f

*/
