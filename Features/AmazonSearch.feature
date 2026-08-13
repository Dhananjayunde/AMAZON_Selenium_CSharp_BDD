Feature: Amazon Product Search

  Scenario: Search for Vivo X300 on Amazon
    Given I navigate to Amazon
    When I search for "Vivo X300"
    Then Amazon search results should be displayed