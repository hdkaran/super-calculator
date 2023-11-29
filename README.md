# super-calculator
Coding challenge for yellow canary.

## Instructions to Run
- Similar to any .NET repo, clone and build on your preferred IDE
- Similary tests can be run on any IDE that supports .NET development
- application uses swagger, so after if you're using custom launch settings, proceed to "/swagger" in your browser when application starts.

## Assumptions
- The format of excel file being fed in will not change, the application assumes a format that was provided in sample dataset
- the employee ids are always a number and not strings
- ote_treatment for Paycodes can only be either Not OTE or OTE, this field is *Case Sensitive*
- also assuming the excel sheet will have a typo when uploaded for the header of "ote_treament"
- max file size has been assumed but does not affects functionality

## Other Notes
- The application will scan the sheet for an employee for all quarters they have worked in
- it will break down each employees quarterly super pay cycle and return variances in the required format
- Application ignores if there are periods of non work by the employee in any quarter. For example, employee worked in Q1 and Q3, application will ignore Q2 calculations to save on performance
- Error handling could have been made more detailed, but the setup of error handling provides for general plumbing
- Similarly validations can be done more detailed, but the setup provides a base.
